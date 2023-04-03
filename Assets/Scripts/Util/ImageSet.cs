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

        public Texture2D GetTexture2DFromIndex(ushort index)
        {
            Sprite sprite = GetSpriteFromIndex(index);
            if (sprite == null)
                return null;

            int width = (int)sprite.rect.width;
            int height = (int)sprite.rect.height;
            int x = (int)sprite.rect.x;
            int y = (int)sprite.rect.y;

            Texture2D texture = new Texture2D(width, height);
            texture.alphaIsTransparency = true;
            texture.filterMode = FilterMode.Point;
            Color[] colors = sprite.texture.GetPixels(x, y, width, height);
            texture.SetPixels(colors, 0);
            texture.Apply();

            return texture;
        }

        public int SpriteCount()
        {
            return sprites.Count;
        }
    }
}
