using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotmgClient.Game
{
    public class GameSprite : MonoBehaviour
    {
        public static GameSprite Instance;

        private int lastUpdate;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
