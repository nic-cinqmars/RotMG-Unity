using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotmgClient.Map
{
    public class GroundTile
    {
        private int x;
        private int y;
        private ushort tileType = 0xFF;
        private GroundProperties groundProperties;
        public Sprite sprite;
        private int sink = 0;
        
        public GroundTile(int x, int y)
        {
            groundProperties = GroundLibrary.defaultProperties;
            this.x = x;
            this.y = y;
        }

        public void setTileType(ushort tileType)
        {
            this.tileType = tileType;
            groundProperties = GroundLibrary.propertiesLibrary[tileType];
            //sprite = GroundLibrary.
        }
    }
}
