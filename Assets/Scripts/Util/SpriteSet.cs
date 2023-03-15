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

        public SpriteSet(Texture2D spriteSheet, int spriteWidth, int spriteHeight)
        {
            sprites = new List<Sprite>();
            for (int y = 1; y < (spriteSheet.height / spriteHeight) + 1; y++)
            {
                for (int x = 0; x < spriteSheet.width / spriteWidth; x++)
                {
                    Rect rect = new Rect(x * spriteWidth, spriteSheet.height - (y * spriteHeight), spriteWidth, spriteHeight);
                    sprites.Add(Sprite.Create(spriteSheet, rect, new Vector2(0.5f, 0.5f), 8));
                }
            }
        }

        /* Old
        public SpriteSet(string spriteSheet)
        {
            sprites = new List<Sprite>();
            Sprite[] loadedSprites = Resources.LoadAll<Sprite>(spriteSheet);
            foreach (Sprite sprite in loadedSprites)
            {
                sprites.Add(sprite);
            }
        }
        */

        public Sprite GetSpriteFromIndex(ushort index)
        {
            if (index >= sprites.Count)
                return null;

            return sprites[index];
        }
    }
}
