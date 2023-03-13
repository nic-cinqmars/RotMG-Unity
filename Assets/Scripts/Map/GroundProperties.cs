using Org.BouncyCastle.Math.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEditor.Experimental.GraphView;

namespace RotmgClient.Map
{
    public class GroundProperties
    {
        public ushort type;
        public string id;
        public bool noWalk = true;
        public int minDamage = 0;
        public int maxDamage = 0;
        // public AnimateProperties animateProperties
        public int blendPriority = -1;
        public int compositePriority = 0;
        public float speed = 1;
        public float xOffset = 0;
        public float yOffset = 0;
        public float slideAmount = 0;
        public bool push = false;
        public bool sink = false;
        public bool sinking = false;
        public bool randomOffset = false;
        public bool hasEdge = false;
        
        public GroundProperties(XmlNode xml)
        {
            type = Convert.ToUInt16(xml.Attributes["type"].Value, 16);
            id = xml.Attributes["id"].Value;
            if (xml.SelectSingleNode("NoWalk") != null )
                noWalk = true;
            if (xml.SelectSingleNode("MinDamage") != null)
                minDamage = int.Parse(xml.SelectSingleNode("MinDamage").InnerText);
            if (xml.SelectSingleNode("MaxDamage") != null)
                maxDamage = int.Parse(xml.SelectSingleNode("MaxDamage").InnerText);
            if (xml.SelectSingleNode("Push") != null)
                push = true;
            if (xml.SelectSingleNode("BlendPriority") != null)
                blendPriority = int.Parse(xml.SelectSingleNode("BlendPriority").InnerText);
            if (xml.SelectSingleNode("CompositePriority") != null)
                compositePriority = int.Parse(xml.SelectSingleNode("CompositePriority").InnerText);
            if (xml.SelectSingleNode("Speed") != null)
                speed = float.Parse(xml.SelectSingleNode("Speed").InnerText);
            if (xml.SelectSingleNode("SlideAmount") != null)
                slideAmount = float.Parse(xml.SelectSingleNode("SlideAmount").InnerText);
            if (xml.SelectSingleNode("XOffset") != null)
                xOffset = float.Parse(xml.SelectSingleNode("XOffset").InnerText);
            if (xml.SelectSingleNode("YOffset") != null)
                yOffset = float.Parse(xml.SelectSingleNode("YOffset").InnerText);
            if (xml.SelectSingleNode("Sink") != null)
                sink = true;
            if (xml.SelectSingleNode("Sinking") != null)
                sinking = true;
            if (xml.SelectSingleNode("RandomOffset") != null)
                randomOffset = true;
            if (xml.SelectSingleNode("HasEdge") != null)
                hasEdge = true;
        }
    }
}
