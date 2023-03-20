using RotmgClient.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace RotmgClient.Objects
{
    public class RotmgGameObject : MonoBehaviour
    {
        private GameMap map;
        private float x = 0;
        private float y = 0;

        private int myLastTickId = 0;
        private int lastTickUpdateTime = 0;

        public Vector2 tickPosition = Vector2.zero;
        public Vector2 posAtTick = Vector2.zero;
        public Vector2 movementVector = Vector2.zero;

        protected void Update()
        {
            if (movementVector.x != 0 || movementVector.y != 0)
            {
                Debug.Log("RotmgGameObjectUpdate");
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
