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
        private List<TextureData> randomTextureData = null;
        private Sprite mask;
        private AnimatedChar animatedChar;

        public TextureData()
        {
            textureSprite = AssetLibrary.GetUnknownSprite();
            mask = null;
            animatedChar = null;
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

            XmlNode randomTextureNode = xml.SelectSingleNode("RandomTexture");
            if (randomTextureNode != null)
            {
                randomTextureData = new List<TextureData>();
                foreach (XmlNode texture in randomTextureNode.SelectNodes("Texture"))
                {
                    randomTextureData.Add(new TextureData(texture));
                }
            }

            XmlNode maskNode = xml.SelectSingleNode("Mask");
            if (maskNode != null)
            {
                mask = AssetLibrary.GetSpriteFromSet(maskNode.SelectSingleNode("File").InnerText,
                    Convert.ToUInt16(maskNode.SelectSingleNode("Index").InnerText, 16));
            }

            XmlNode animatedTextureNode = xml.SelectSingleNode("AnimatedTexture");
            if (animatedTextureNode != null)
            {
                animatedChar = AnimatedChars.GetAnimatedChar(animatedTextureNode.SelectSingleNode("File").InnerText,
                    Convert.ToUInt16(animatedTextureNode.SelectSingleNode("Index").InnerText, 16));
            }
        }

        public Sprite GetTextureSprite(int random = 0)
        {
            if (randomTextureData == null)
                return textureSprite;

            TextureData textureData = randomTextureData[random % randomTextureData.Count];
            return textureData.GetTextureSprite();
        }

        public AnimatedChar GetAnimatedChar()
        {
            return animatedChar;
        }

        public List<TextureData> GetRandomTextureData()
        {
            return randomTextureData;
        }

        public Sprite GetMask()
        {
            return mask;
        }
    }
}
