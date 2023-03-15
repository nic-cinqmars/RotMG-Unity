using RotmgClient.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace RotmgClient.Util
{
    public class TextureData
    {
        private Sprite textureSprite;

        public TextureData()
        {
            textureSprite = AssetLibrary.GetUnknownSprite();
        }

        public TextureData(XmlNode xml)
        {
            textureSprite = AssetLibrary.GetUnknownSprite();

            XmlNode textureNode = xml.SelectSingleNode("Texture");
            if (textureNode != null)
            {
                string spriteSetID = textureNode.SelectSingleNode("File").InnerText;
                ushort spriteIndex = Convert.ToUInt16(textureNode.SelectSingleNode("Index").InnerText, 16);

                textureSprite = AssetLibrary.GetSpriteFromSet(spriteSetID, spriteIndex);
            }
        }

        public Sprite GetTextureSprite()
        {
            return textureSprite;
        }
    }
}
