using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class SpriteSet
    {
        private List<Sprite> sprites;

        public SpriteSet(string spriteSheet)
        {
            sprites = new List<Sprite>();
            Sprite[] loadedSprites = Resources.LoadAll<Sprite>(spriteSheet);
            foreach (Sprite sprite in loadedSprites)
            {
                sprites.Add(sprite);
            }
        }

        public SpriteSet()
        {
            Texture2D texture2D = Resources.Load<Texture2D>("Sprites/lofiChar");
            List<Sprite> sprites = new List<Sprite>();
            for (int x = 0; x < texture2D.width / 8; x++) 
            {
                for (int y = 0; y < texture2D.height / 8; y++)
                {
                    Rect rect = new Rect(x * 8, y * 8, 8, 8);
                    sprites.Add(Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f)));
                }
            }
        }

        public Sprite GetSpriteFromIndex(ushort index)
        {
            if (index > sprites.Count)
                return null;

            return sprites[index];
        }
    }
}
