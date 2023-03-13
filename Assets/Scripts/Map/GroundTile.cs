using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RotmgClient.Map
{
    public class GroundTile
    {
        private int x;
        private int y;
        private ushort tileType = 0xFF;
        private GroundProperties groundProperties;
        private Sprite sprite;
        private int sink = 0;
        
        public GroundTile(int x, int y)
        {
            groundProperties = GroundLibrary.GetDefaultProperties();
            this.x = x;
            this.y = y;
        }

        public void SetTileType(ushort tileType)
        {
            this.tileType = tileType;
            groundProperties = GroundLibrary.GetPropertiesFromType(tileType);
            sprite = GroundLibrary.GetSpriteFromType(tileType);
        }

        public ushort GetTileType()
        {
            return tileType;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }
    }
}
