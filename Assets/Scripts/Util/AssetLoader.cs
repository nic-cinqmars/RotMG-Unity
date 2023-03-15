using Assets.Scripts.Util;
using RotmgClient.Map;
using RotmgClient.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace RotmgClient.Util
{
    public class AssetLoader
    {
        public void Load()
        {
            AddImages();
            ParseGroundFiles();
            ParseObjectFiles();
        }

        public void AddImages()
        {
            AssetLibrary.AddSpriteSet("lofiEnvironment", "Sprites/lofiEnvironment");
            AssetLibrary.AddSpriteSet("lofiEnvironment2", "Sprites/lofiEnvironment2");
            AssetLibrary.AddSpriteSet("lofiEnvironment3", "Sprites/lofiEnvironment3");
            AssetLibrary.AddSpriteSet("lofiObj3", "Sprites/lofiObj3");
            AssetLibrary.AddSpriteSet("d3LofiObjEmbed", "Sprites/d3LofiObj");
            AssetLibrary.AddSpriteSet("invisible", "Sprites/Invisible");
        }

        public void ParseGroundFiles()
        {
            TextAsset xml = Resources.Load<TextAsset>("Data/GroundCXML");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);
            GroundLibrary.ParseFromXML(doc);
        }

        public void ParseObjectFiles()
        {
            TextAsset xml = Resources.Load<TextAsset>("Data/ObjectsCXML");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);
            ObjectLibrary.ParseFromXML(doc);
        }
    }
}
