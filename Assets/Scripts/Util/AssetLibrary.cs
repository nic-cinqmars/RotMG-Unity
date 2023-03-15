using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Util
{
    public static class AssetLibrary
    {
        private static readonly Sprite unknownSprite = Resources.Load<Sprite>("Sprites/UnsetTexture");
        private static readonly Dictionary<string, SpriteSet> spriteSets = new Dictionary<string, SpriteSet>();

        public static void AddSpriteSet(string spriteSetID, string spriteSheetPath)
        {
            SpriteSet spriteSet = new SpriteSet(spriteSheetPath);
            spriteSets.Add(spriteSetID, spriteSet);
        }

        public static Sprite GetSpriteFromSet(string spriteSetID, ushort index)
        {
            if (spriteSets.TryGetValue(spriteSetID, out SpriteSet spriteSet))
            {
                Sprite sprite = spriteSet.GetSpriteFromIndex(index);
                if (sprite == null)
                {
                    Debug.LogErrorFormat("Could not find sprite at index '{0}' in sprite set with id '{1}'!", "0x" + index.ToString("x"), spriteSetID);
                    sprite = unknownSprite;
                }
                return sprite;
            }
            else
            {
                Debug.LogErrorFormat("Could not find sprite set with id '{0}'!", spriteSetID);
                return unknownSprite;
            }
        }

        public static Sprite GetUnknownSprite()
        {
            return unknownSprite;
        }

    }
}
