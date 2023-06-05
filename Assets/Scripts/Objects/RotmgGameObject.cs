using RotmgClient.Networking;
using RotmgClient.Util;
using System;
using System.Collections.Generic;
using System.IO;
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

        public Vector2 targetPostion;
        public Vector2 direction;

        public virtual void Setup(XmlNode xml)
        {
            Debug.Log("Calling RotmgGameObject setup");
            x = 0;
            y = 0;
            targetPostion = Vector2.zero;
            direction = Vector2.zero;
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
            if (direction.x != 0 || direction.y != 0)
            {
                float directionX = direction.x * Time.deltaTime * 1000;
                float directionY = direction.y * Time.deltaTime * 1000;

                float newX = x + directionX;
                float newY = y + directionY;

                if ((direction.x > 0 && newX > targetPostion.x) || 
                    (direction.x < 0 && newX < targetPostion.x))
                {
                    newX = targetPostion.x;
                    direction.x = 0;
                }

                if ((direction.y > 0 && newY > targetPostion.y) ||
                    (direction.y < 0 && newY < targetPostion.y))
                {
                    newY = targetPostion.y;
                    direction.y = 0;
                }

                moveTo(new Vector2(newX, newY));
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
            targetPostion = position;
            direction = Vector2.zero;
            moveTo(position);
        }

        public void OnTickPos(float x, float y) 
        {
            if (targetPostion.x == x && targetPostion.y == y)
                return;

            targetPostion.x = x;
            targetPostion.y = y;
            direction.x = (targetPostion.x - this.x) / 127; //magic according to Skilly?
            direction.y = (targetPostion.y - this.y) / 127;
        }

        public void moveTo(Vector2 newPos)
        {
            x = newPos.x;
            y = newPos.y;
            gameObject.transform.localPosition = new Vector2(newPos.x, newPos.y);
        }
    }
}
