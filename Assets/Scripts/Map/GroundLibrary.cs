using RotmgClient.Util;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace RotmgClient.Map
{
    public static class GroundLibrary
    {
        private static readonly Dictionary<ushort, GroundProperties> propertiesLibrary = new Dictionary<ushort, GroundProperties>();
        private static GroundProperties defaultProperties;
        private static readonly Dictionary<ushort, TextureData> textureDataLibrary = new Dictionary<ushort, TextureData>();

        public static void ParseFromXML(XmlDocument xml)
        {
            foreach (XmlNode node in xml.DocumentElement.ChildNodes)
            {
                ushort type = Convert.ToUInt16(node.Attributes["type"].Value, 16);

                GroundProperties groundProperties = new GroundProperties(node);
                propertiesLibrary.Add(type, groundProperties);

                TextureData textureData = new TextureData(node);
                textureDataLibrary.Add(type, textureData);

            }
            if (propertiesLibrary.TryGetValue(0xFF, out GroundProperties defaultProp))
            {
                defaultProperties = defaultProp;
            }
        }

        public static Sprite GetSpriteFromType(ushort tileType)
        {
            if (textureDataLibrary.TryGetValue(tileType, out TextureData textureData))
            {
                return textureData.GetTextureSprite();
            }
            else
            {
                Debug.LogErrorFormat("Could not find tile sprite with type '{0}'. Setting '{0}' to 'UnsetTexture' sprite.", "0x" + tileType.ToString("x"));
                Sprite sprite = Resources.Load<Sprite>("Sprites/UnsetTexture");
                textureDataLibrary[tileType] = new TextureData();
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
                Debug.LogErrorFormat("Could not find ground properties for tile with type '{0}'. Setting '{0}' to default tile properties.", "0x" + tileType.ToString("x"));
                propertiesLibrary[tileType] = defaultProperties;
                return defaultProperties;
            }
        }
    }
}
