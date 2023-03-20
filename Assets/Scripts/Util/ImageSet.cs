using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Util
{
    public class ImageSet
    {
        private List<Sprite> sprites;

        public ImageSet(Texture2D spriteSheet, int spriteWidth, int spriteHeight)
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

        public Sprite GetSpriteFromIndex(ushort index)
        {
            if (index >= sprites.Count)
                return null;

            return sprites[index];
        }

        public int SpriteCount()
        {
            return sprites.Count;
        }
    }
}
