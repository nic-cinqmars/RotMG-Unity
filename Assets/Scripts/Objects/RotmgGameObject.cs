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
        private float x = 0;
        private float y = 0;

        private int myLastTickId = 0;
        private int lastTickUpdateTime = 0;

        private Vector2 tickPosition = Vector2.zero;
        private Vector2 posAtTick = Vector2.zero;
        private Vector2 movementVector = Vector2.zero;

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
            gameObject.transform.position = new Vector2(newPos.x, newPos.y);
        }
    }
}
