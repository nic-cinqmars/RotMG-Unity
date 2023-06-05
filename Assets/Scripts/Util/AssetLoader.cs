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
        private readonly string GROUND_PATH = "Data/Ground/";
        private readonly string OBJECT_PATH = "Data/Object/";

        private readonly string[] GROUND_FILES = { "GroundCXML" };
        private readonly string[] OBJECT_FILES = { "ProjectilesCXML", "EquipCXML", "DyesCXML", "TextilesCXML", "PermapetsCXML", 
            "PlayersCXML", "ObjectsCXML", "TestingObjectsCXML", "StaticObjectsCXML", "TutorialObjectsCXML", "MonstersCXML",
            "PetsCXML", "TempObjectsCXML", "LowCXML", "MidCXML", "HighCXML", "MountainsCXML", "EncountersCXML", "OryxCastleCXML",
            "TombOfTheAncientsCXML", "SpriteWorldCXML", "UndeadLairCXML", "OceanTrenchCXML", "ForbiddenJungleCXML", "OryxChamberCXML",
            "OryxWineCellarCXML", "ManorOfTheImmortalsCXML", "PirateCaveCXML", "SnakePitCXML", "AbyssOfDemonsCXML", "GhostShipCXML",
            "MadLabCXML", "CaveOfAThousandTreasuresCXML", "CandyLandCXML", "HauntedCemeteryCXML" };

        public void Load()
        {
            AddImages();
            AddAnimatedCharacters();
            ParseGroundFiles();
            ParseObjectFiles();
        }

        public void AddImages()
        {
            AssetLibrary.AddSpriteSet("lofiChar8x8", "Sprites/lofiChar", 8, 8);
            AssetLibrary.AddSpriteSet("lofiChar16x8", "Sprites/lofiChar", 16, 8);
            AssetLibrary.AddSpriteSet("lofiChar16x16", "Sprites/lofiChar", 16, 16);
            AssetLibrary.AddSpriteSet("lofiChar28x8", "Sprites/lofiChar2", 8, 8);
            AssetLibrary.AddSpriteSet("lofiChar216x8", "Sprites/lofiChar2", 16, 8);
            AssetLibrary.AddSpriteSet("lofiChar216x16", "Sprites/lofiChar2", 16, 16);
            AssetLibrary.AddSpriteSet("lofiCharBig", "Sprites/lofiCharBig", 16, 16);
            AssetLibrary.AddSpriteSet("lofiEnvironment", "Sprites/lofiEnvironment", 8, 8);
            AssetLibrary.AddSpriteSet("lofiEnvironment2", "Sprites/lofiEnvironment2", 8, 8);
            AssetLibrary.AddSpriteSet("lofiEnvironment3", "Sprites/lofiEnvironment3", 8, 8);
            AssetLibrary.AddSpriteSet("lofiInterface", "Sprites/lofiInterface", 8, 8);
            AssetLibrary.AddSpriteSet("lofiInterfaceBig", "Sprites/lofiInterfaceBig", 16, 16);
            AssetLibrary.AddSpriteSet("lofiInterface2", "Sprites/lofiInterface2", 8, 8);
            AssetLibrary.AddSpriteSet("redLootBag", "Sprites/redLootBag", 8, 8);
            AssetLibrary.AddSpriteSet("lofiObj", "Sprites/lofiObj", 8, 8);
            AssetLibrary.AddSpriteSet("lofiObj2", "Sprites/lofiObj2", 8, 8);
            AssetLibrary.AddSpriteSet("lofiObj3", "Sprites/lofiObj3", 8, 8);
            AssetLibrary.AddSpriteSet("lofiObj4", "Sprites/lofiObj4", 8, 8);
            AssetLibrary.AddSpriteSet("lofiObj5", "Sprites/lofiObj5", 8, 8);
            AssetLibrary.AddSpriteSet("lofiObj6", "Sprites/lofiObj6", 8, 8);
            AssetLibrary.AddSpriteSet("lofiObjBig", "Sprites/lofiObjBig", 16, 16);
            AssetLibrary.AddSpriteSet("lofiObj40x40", "Sprites/lofiObj40x40", 40, 40);
            AssetLibrary.AddSpriteSet("lofiProjs", "Sprites/lofiProjs", 8, 8);
            AssetLibrary.AddSpriteSet("lofiProjsBig", "Sprites/lofiProjsBig", 16, 16);
            AssetLibrary.AddSpriteSet("lofiParts", "Sprites/lofiParts", 8, 8);
            AssetLibrary.AddSpriteSet("stars", "Sprites/stars", 5, 5);
            AssetLibrary.AddSpriteSet("textile4x4", "Sprites/textile4x4", 4, 4);
            AssetLibrary.AddSpriteSet("textile5x5", "Sprites/textile5x5", 5, 5);
            AssetLibrary.AddSpriteSet("textile9x9", "Sprites/textile9x9", 9, 9);
            AssetLibrary.AddSpriteSet("textile10x10", "Sprites/textile10x10", 10, 10);
            AssetLibrary.AddSpriteSet("inner_mask", "Sprites/innerMask", 4, 4);
            AssetLibrary.AddSpriteSet("sides_mask", "Sprites/sidesMask", 4, 4);
            AssetLibrary.AddSpriteSet("outer_mask", "Sprites/outerMask", 4, 4);
            AssetLibrary.AddSpriteSet("innerP1_mask", "Sprites/innerP1Mask", 4, 4);
            AssetLibrary.AddSpriteSet("innerP2_mask", "Sprites/innerP2Mask", 4, 4);
            AssetLibrary.AddSpriteSet("invisible", "Sprites/Invisible", 8, 8);
            AssetLibrary.AddSpriteSet("cursorsEmbed", "Sprites/cursors", 32, 32);
        }

        public void AddAnimatedCharacters()
        {
            AnimatedChars.AddAnimatedChar("players", "Sprites/players", "Sprites/playersMask", 56, 24, 8, 8, AnimatedChar.RIGHT);
        }

        public void ParseGroundFiles()
        {
            foreach (string groundFile in GROUND_FILES)
            {
                Debug.LogFormat("Adding ground xml with id '{0}'.", groundFile);
                TextAsset xml = Resources.Load<TextAsset>(GROUND_PATH + groundFile);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.text);
                GroundLibrary.ParseFromXML(doc);
            }
        }

        public void ParseObjectFiles()
        {
            foreach (string objectFile in OBJECT_FILES)
            {
                Debug.LogFormat("Adding object xml with id '{0}'.", objectFile);
                TextAsset xml = Resources.Load<TextAsset>(OBJECT_PATH + objectFile);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.text);
                ObjectLibrary.ParseFromXML(doc);
            }
        }
    }
}
