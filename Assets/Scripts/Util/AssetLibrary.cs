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
        private static readonly Dictionary<string, Texture2D> spriteSheets = new Dictionary<string, Texture2D>();
        private static readonly Dictionary<string, ImageSet> spriteSets = new Dictionary<string, ImageSet>();

        public static void AddSpriteSet(string spriteSetID, string spriteSheetPath, int spriteWidth, int spriteHeight)
        {
            Debug.LogFormat("Adding sprite sheet with id '{0}'.", spriteSetID);
            Texture2D currentSpriteSheet;
            if (spriteSheets.TryGetValue(spriteSheetPath, out Texture2D spriteSheet))
            {
                currentSpriteSheet = spriteSheet;
            }
            else
            {
                Texture2D newSpriteSheet = Resources.Load<Texture2D>(spriteSheetPath);
                spriteSheets.Add(spriteSheetPath, newSpriteSheet);
                currentSpriteSheet = newSpriteSheet;
            }

            ImageSet spriteSet = new ImageSet(currentSpriteSheet, spriteWidth, spriteHeight);
            spriteSets.Add(spriteSetID, spriteSet);
        }

        public static Sprite GetSpriteFromSet(string spriteSetID, ushort index)
        {
            if (spriteSets.TryGetValue(spriteSetID, out ImageSet spriteSet))
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
