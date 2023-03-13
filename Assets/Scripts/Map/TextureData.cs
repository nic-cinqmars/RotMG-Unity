using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace RotmgClient.Map
{
    public class TextureData
    {
        private Sprite texture;

        public TextureData(XmlNode xml)
        {
            if (xml.SelectSingleNode("Texture") != null)
            {
                XmlNode textureNode = xml.SelectSingleNode("Texture");
            }
                
        }
    }
}
