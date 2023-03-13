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

        public Sprite GetSpriteFromIndex(ushort index)
        {
            return sprites[index];
        }
    }
}
