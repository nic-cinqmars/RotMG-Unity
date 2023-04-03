using RotmgClient.Networking;
using RotmgClient.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RotmgClient.Objects
{
    public class RotmgGameObject : MonoBehaviour
    {
        private ushort objectType;
        private GameMap map;
        private float x = 0;
        private float y = 0;
        private ObjectProperties objectProperties;

        private List<TextureData> randomTextureData = null;
        private AnimatedChar animatedChar = null;
        private Sprite sprite = null;
        private Sprite mask = null;

        private int myLastTickId = -1;
        private int lastTickUpdateTime = 0;

        public Vector2 posAtTick;
        public Vector2 tickPosition;
        public Vector2 movementVector;

        public virtual void Setup(XmlNode xml)
        {
            Debug.Log("Calling RotmgGameObject setup");
            x = 0;
            y = 0;
            posAtTick = Vector2.zero;
            tickPosition = Vector2.zero;
            movementVector = Vector2.zero;
            objectProperties = new ObjectProperties();

            if (xml == null)
                return;

            objectType = Convert.ToUInt16(xml.Attributes["type"].Value, 16);
            objectProperties = ObjectLibrary.GetPropertiesFromType(objectType);

            TextureData textureData = ObjectLibrary.GetTextureDataFromType(objectType);
            sprite = textureData.GetTextureSprite();
            animatedChar = textureData.GetAnimatedChar();
            mask = textureData.GetMask();
            randomTextureData = textureData.GetRandomTextureData();
        }

        protected void Update()
        {
            if (movementVector.x != 0 || movementVector.y != 0)
            {
                if (myLastTickId < Client.Instance.lastTickId)
                {
                    movementVector.x = 0;
                    movementVector.y = 0;
                    moveTo(tickPosition);
                }
                else
                {
                    int offset = Client.Instance.lastUpdate - lastTickUpdateTime;
                    Vector2 newPos = new Vector2(posAtTick.x + (offset * movementVector.x), posAtTick.y + (offset * movementVector.y));
                    moveTo(newPos);
                }
            }
        }

        private Sprite GetTexture()
        {
            if (animatedChar != null)
            {
                MaskedImage image = animatedChar.GetImageFromDir(AnimatedChar.RIGHT, AnimatedChar.STAND);
                Sprite sprite = Sprite.Create(image.GetImage(), new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
                return sprite;
            }
            else
            {
                return sprite;
            }
        }

        public void Draw()
        {
            GetComponent<SpriteRenderer>().sprite = GetTexture();
        }

        public ushort GetObjectType()
        {
            return objectType;
        }

        public void AddTo(GameMap map, Vector2 position)
        {
            this.map = map;
            tickPosition = position;
            posAtTick = position;
            moveTo(position);
        }

        public void OnTickPos(float x, float y, int tickTime, int tickID) 
        {
            if (myLastTickId < Client.Instance.lastTickId) 
                moveTo(tickPosition);

            lastTickUpdateTime = Client.Instance.lastUpdate;
            tickPosition.x = x;
            tickPosition.y = y;
            posAtTick.x = this.x;
            posAtTick.y = this.y;
            movementVector.x = ((tickPosition.x - posAtTick.x) / tickTime);
            movementVector.y = ((tickPosition.y - posAtTick.y) / tickTime);
            myLastTickId = tickID;
        }

        public void moveTo(Vector2 newPos)
        {
            x = newPos.x;
            y = newPos.y;
            gameObject.transform.localPosition = new Vector2(newPos.x, newPos.y);
        }
    }
}
