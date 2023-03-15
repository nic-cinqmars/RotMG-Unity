using RotmgClient.Map;
using RotmgClient.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RotmgClient.Objects
{
    public static class ObjectLibrary
    {
        private static readonly string SPRITE_NOT_FOUND_SET_NAME = "lofiObj3";
        private static readonly ushort SPRITE_NOT_FOUND_INDEX = 0xFF;
        private static readonly Dictionary<ushort, ObjectProperties> propertiesLibrary = new Dictionary<ushort, ObjectProperties>();
        private static ObjectProperties defaultProperties = new ObjectProperties();
        private static readonly Dictionary<ushort, TextureData> textureDataLibrary = new Dictionary<ushort, TextureData>();
        private static readonly Dictionary<ushort, TextureData> topTextureDataLibrary = new Dictionary<ushort, TextureData>();
        private static readonly Dictionary<string, ushort> idToTypeLibrary = new Dictionary<string, ushort>();
        private static readonly Dictionary<ushort, string> typeToClassName = new Dictionary<ushort, string>();
        private static readonly Dictionary<ushort, string> typeToDisplayIdLibrary = new Dictionary<ushort, string>();
        //private static readonly Dictionary<ushort, AnimationsData> animationsDataLibrary = new Dictionary<ushort, AnimationsData>();

        public static void ParseFromXML(XmlDocument xml)
        {
            foreach (XmlNode node in xml.DocumentElement.SelectNodes("Object"))
            {
                ushort type = Convert.ToUInt16(node.Attributes["type"].Value, 16);
                string id = node.Attributes["id"].Value;
                string className = "Unknown";
                try
                {
                    className = node.SelectSingleNode("Class").InnerText;
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogErrorFormat("Object with type '{0}' has no class!", "0x" + type.ToString("x"));
                }

                if (!typeToClassName.TryAdd(type, className))
                {
                    UnityEngine.Debug.LogErrorFormat("Already added '{0}' to '{1}'!", "0x" + type.ToString("x"), className);
                }

                string displayId = id;
                if (node.SelectSingleNode("DisplayId") != null)
                {
                    displayId = node.SelectSingleNode("DisplayId").InnerText;
                }

                if (node.SelectSingleNode("Group") != null)
                {
                    if (node.SelectSingleNode("Group").InnerText == "Hexable")
                    {
                        //Todo
                    }
                }

                if (node.SelectSingleNode("PetBehavior") != null || node.SelectSingleNode("PetAbility") != null)
                {
                    //Todo
                }
                else
                {
                    ObjectProperties objectProperties = new ObjectProperties(node);
                    propertiesLibrary.Add(type, objectProperties);

                    idToTypeLibrary.Add(id, type);
                    typeToDisplayIdLibrary.Add(type, displayId);

                    if (className == "Player")
                    {
                        //Todo
                        UnityEngine.Debug.Log("Got class");
                    }

                    TextureData textureData = new TextureData(node);
                    textureDataLibrary.Add(type, textureData);

                    if (node.SelectSingleNode("Top") != null)
                    {
                        TextureData topTextureData = new TextureData(node.SelectSingleNode("Top"));
                        topTextureDataLibrary.Add(type, topTextureData);
                    }

                    if (node.SelectSingleNode("Animation") != null)
                    {
                        // Todo
                        // Add AnimationsData to library
                    }
                }
            }
        }

        public static ObjectProperties GetPropertiesFromType(ushort objectType)
        {
            if (propertiesLibrary.TryGetValue(objectType, out ObjectProperties objectProperties))
            {
                return objectProperties;
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Could not find object properties for object with type '{0}'. Setting '{0}' to default object properties.", "0x" + objectType.ToString("x"));
                propertiesLibrary[objectType] = defaultProperties;
                return defaultProperties;
            }
        }

        public static GameObject GetObjectInstanceFromType(ushort objectType)
        {
            if (typeToClassName.TryGetValue(objectType, out string className))
            {
                return new GameObject(className);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Could not find class name for object with type '{0}'. Setting class name to 'Unknown'.", "0x" + objectType.ToString("x"));
                typeToClassName[objectType] = "Unknown";
                return new GameObject("Unknown");
            }
        }

        public static Sprite GetSpriteFromType(ushort objectType)
        {
            if (textureDataLibrary.TryGetValue(objectType, out TextureData textureData))
            {
                return textureData.GetTextureSprite();
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Could not find object sprite with type '{0}'. Setting '{0}' to 'UnsetTexture' sprite.", "0x" + objectType.ToString("x"));
                Sprite sprite = Resources.Load<Sprite>("Sprites/UnsetTexture");
                textureDataLibrary[objectType] = new TextureData();
                return sprite;
            }
        }

        public static string GetIdFromType(ushort objectType)
        {
            if (typeToDisplayIdLibrary.TryGetValue(objectType, out string objectId))
            {
                return objectId;
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Could not find id for object with type '{0}'. Setting to 'Unknown'.", "0x" + objectType.ToString("x"));
                typeToDisplayIdLibrary[objectType] = "Unknown";
                return "Unknown";
            }
        }
    }
}
