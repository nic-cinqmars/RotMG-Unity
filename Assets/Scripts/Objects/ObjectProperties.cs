using RotmgClient.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace RotmgClient.Objects
{
    public class ObjectProperties
    {
        private ushort type;
        private string id;
        private string displayId;
        private int shadowSize;
        private bool isPlayer = false;
        private bool isEnemy = false;
        private bool drawOnGround = false;
        private bool drawUnder = false;
        private bool occupySquare = false;
        private bool fullOccupy = false;
        private bool enemyOccupySquare = false;
        private bool isStatic = false;
        private bool noMinimap = false;
        private bool protectFromGroundDamage = false;
        private bool protectFromSink = false;
        private float z = 0;
        private bool flying = false;
        private uint color = 0xFFFFFF;
        private bool showName = false;
        private bool dontFaceAttacks = false;
        private float bloodProb = 0;
        private uint bloodColor = 0xFF0000;
        private uint shadowColor = 0;
        private object sounds = null;
        private TextureData portrait;
        private int minSize = 100;
        private int maxSize = 100;
        private int sizeStep = 5;
        private WhileMovingProperties whileMoving = null;
        private string oldSound = null;
        //private Dictionary<Projectile>
        private float angleCorrection = 0;
        private float rotation = 0;

        public ObjectProperties()
        {

        }

        public ObjectProperties(XmlNode xml)
        {
            type = Convert.ToUInt16(xml.Attributes["type"].Value, 16);
            id = xml.Attributes["id"].Value;
            displayId = id;
            if (xml.SelectSingleNode("DisplayId") != null)
                displayId = xml.SelectSingleNode("DisplayId").InnerText;
            if (xml.SelectSingleNode("ShadowSize") != null)
                shadowSize = int.Parse(xml.SelectSingleNode("ShadowSize").InnerText);
            if (xml.SelectSingleNode("Player") != null)
                isPlayer = true;
            if (xml.SelectSingleNode("Enemy") != null)
                isEnemy = true;
            if (xml.SelectSingleNode("DrawOnGround") != null)
                drawOnGround = true;
            if (drawOnGround || xml.SelectSingleNode("DrawUnder") != null)
                drawUnder = true;
            if (xml.SelectSingleNode("OccupySquare") != null)
                occupySquare = true;
            if (xml.SelectSingleNode("FullOccupy") != null)
                fullOccupy = true;
            if (xml.SelectSingleNode("EnemyOccupySquare") != null)
                enemyOccupySquare = true;
            if (xml.SelectSingleNode("Static") != null)
                isStatic = true;
            if (xml.SelectSingleNode("NoMiniMap") != null)
                noMinimap = true;
            if (xml.SelectSingleNode("ProtectFromGroundDamage") != null)
                protectFromGroundDamage = true;
            if (xml.SelectSingleNode("ProtectFromSink") != null)
                protectFromSink = true;
            if (xml.SelectSingleNode("Flying") != null)
                flying = true;
            if (xml.SelectSingleNode("ShowName") != null)
                showName = true;
            if (xml.SelectSingleNode("DontFaceAttacks") != null)
                dontFaceAttacks = true;
            if (xml.SelectSingleNode("Z") != null)
                z = float.Parse(xml.SelectSingleNode("Z").InnerText);
            if (xml.SelectSingleNode("Color") != null)
                color = Convert.ToUInt32(xml.SelectSingleNode("Color").InnerText, 16);
            if (xml.SelectSingleNode("Size") != null)
            {
                maxSize = int.Parse(xml.SelectSingleNode("Size").InnerText);
                minSize = maxSize;
            }
            else
            {
                if (xml.SelectSingleNode("MinSize") != null)
                    minSize = int.Parse(xml.SelectSingleNode("MinSize").InnerText);
                if (xml.SelectSingleNode("MaxSize") != null)
                    maxSize = int.Parse(xml.SelectSingleNode("MaxSize").InnerText);
                if (xml.SelectSingleNode("SizeStep") != null)
                    sizeStep = int.Parse(xml.SelectSingleNode("SizeStep").InnerText);
            }
            if (xml.SelectSingleNode("OldSound") != null)
                oldSound = xml.SelectSingleNode("OldSound").InnerText;
            if (xml.SelectSingleNode("AngleCorrection") != null)
            {
                float angle = float.Parse(xml.SelectSingleNode("AngleCorrection").InnerText);
                angleCorrection = angle * MathF.PI;
            }
            if (xml.SelectSingleNode("Rotation") != null)
                rotation = float.Parse(xml.SelectSingleNode("Rotation").InnerText);
            if (xml.SelectSingleNode("BloodProb") != null)
                bloodProb = float.Parse(xml.SelectSingleNode("BloodProb").InnerText);
            if (xml.SelectSingleNode("BloodColor") != null)
                bloodColor = Convert.ToUInt32(xml.SelectSingleNode("BloodColor").InnerText, 16);
            if (xml.SelectSingleNode("ShadowColor") != null)
                shadowColor = Convert.ToUInt32(xml.SelectSingleNode("ShadowColor").InnerText, 16);

            // Foreach sound in xml sounds

            if (xml.SelectSingleNode("Portrait") != null)
                portrait = new TextureData(xml.SelectSingleNode("Portrait"));
            if (xml.SelectSingleNode("WhileMoving") != null)
                whileMoving = new WhileMovingProperties(xml.SelectSingleNode("WhileMoving"));
        }

        public int GetSize()
        {
            if (minSize == maxSize)
                return minSize;

            int size = (maxSize - minSize) / sizeStep;
            System.Random random = new System.Random();
            return minSize + (int)(random.NextDouble() * size) * sizeStep;
        }
    }

    public class WhileMovingProperties
    {
        public float z = 0;
        public bool flying = false;

        public WhileMovingProperties(XmlNode xml)
        {
            if (xml.SelectSingleNode("Z") != null)
                z = float.Parse(xml.SelectSingleNode("Z").InnerText);

            if (xml.SelectSingleNode("Flying") != null)
                flying = true;
        }

    }
}
