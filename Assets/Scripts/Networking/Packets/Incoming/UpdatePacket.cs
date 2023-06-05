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
        public override PacketId packetId => PacketId.Update;

        public GroundTileData[] Tiles { get; set; }
        public ObjectData[] NewObjs { get; set; }
        public ObjectDropData[] Drops { get; set; }

        public override void Read(NReader nR)
        {
            Tiles = new GroundTileData[nR.ReadInt16()];
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tiles[i] = new GroundTileData();
                Tiles[i].Read(nR);
            }

            NewObjs = new ObjectData[nR.ReadInt16()];
            for (int i = 0; i < NewObjs.Length; i++)
            {
                NewObjs[i] = new ObjectData();
                NewObjs[i].Read(nR);
            }

            Drops = new ObjectDropData[nR.ReadInt16()];
            for (int i = 0; i < Drops.Length; i++)
            {
                Drops[i] = new ObjectDropData();
                Drops[i].Read(nR);
            }
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
