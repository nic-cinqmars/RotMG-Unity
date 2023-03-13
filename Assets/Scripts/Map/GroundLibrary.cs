using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.U2D;

namespace RotmgClient.Map
{
    public static class GroundLibrary
    {
        private static readonly Dictionary<ushort, GroundProperties> propertiesLibrary = new Dictionary<ushort, GroundProperties>();
        private static GroundProperties defaultProperties;
        private static readonly Dictionary<ushort, Sprite> groundSpriteLibrary = new Dictionary<ushort, Sprite>();

        public static void loadSprites()
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/lofiEnvironment2");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == "lofiEnvironment2_144")
                {
                    groundSpriteLibrary.Add(0x3A, sprite);
                }
                else if (sprite.name == "lofiEnvironment2_26")
                {
                    groundSpriteLibrary.Add(0x7A, sprite);
                }
                else if (sprite.name == "lofiEnvironment2_18")
                {
                    groundSpriteLibrary.Add(0xB6, sprite);
                }
                else if (sprite.name == "lofiEnvironment2_10")
                {
                    groundSpriteLibrary.Add(0x7B, sprite);
                }
                else if (sprite.name == "lofiEnvironment2_149")
                {
                    groundSpriteLibrary.Add(0x79, sprite);
                }
            }
        }

        public static void ParseFromXML(XmlDocument xml)
        {
            foreach (XmlNode node in xml.DocumentElement.ChildNodes)
            {
                ushort type = Convert.ToUInt16(node.Attributes["type"].Value, 16);
                GroundProperties groundProperties = new GroundProperties(node);
                propertiesLibrary.Add(type, groundProperties);

            }
            if (propertiesLibrary.TryGetValue(0xFF, out GroundProperties defaultProp))
            {
                defaultProperties = defaultProp;
            }
        }

        public static Sprite GetSpriteFromType(ushort tileType)
        {
            if (groundSpriteLibrary.TryGetValue(tileType, out Sprite tileSprite))
            {
                return tileSprite;
            }
            else
            {
                Debug.LogErrorFormat("Could not find tile sprite with type '{0}'. Setting '{0}' to 'UnsetTexture' sprite.", tileType);
                Sprite sprite = Resources.Load<Sprite>("Sprites/UnsetTexture");
                groundSpriteLibrary[tileType] = sprite;
                return sprite;
            }
        }

        public static GroundProperties GetDefaultProperties()
        {
            return defaultProperties;
        }

        public static GroundProperties GetPropertiesFromType(ushort tileType)
        {
            if (propertiesLibrary.TryGetValue(tileType, out GroundProperties groundProperties))
            {
                return groundProperties;
            }
            else
            {
                Debug.LogErrorFormat("Could not find ground properties for tile with type '{0}'. Setting '{0}' to default tile properties.", tileType);
                propertiesLibrary[tileType] = defaultProperties;
                return defaultProperties;
            }
        }
    }
}
