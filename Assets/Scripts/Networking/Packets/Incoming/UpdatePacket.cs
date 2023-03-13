using RotmgClient.Networking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class UpdatePacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.UPDATE;

        public GroundTileData[] Tiles { get; set; }
        public ObjectData[] NewObjs { get; set; }
        public int[] Drops { get; set; }

        public override void Read(NReader pR)
        {
            Tiles = new GroundTileData[pR.ReadInt16()];
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tiles[i] = new GroundTileData();
                Tiles[i].Read(pR);
            }

            NewObjs = new ObjectData[pR.ReadInt16()];
            for (int i = 0; i < NewObjs.Length; i++)
            {
                NewObjs[i] = new ObjectData();
                NewObjs[i].Read(pR);
            }

            Drops = new int[pR.ReadInt16()];
            for (int i = 0; i < Drops.Length; i++)
            {
                Drops[i] = pR.ReadInt32();
            }
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
