using RotmgClient.Networking.Data;
using System;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class NewTickPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.NEWTICK;

        public int TickId { get; set; }
        public int TickTime { get; set; }
        public ObjectStatusData[] Statuses { get; set; }

        public override void Read(NReader nR)
        {
            TickId = nR.ReadInt32();
            TickTime = nR.ReadInt32();

            Statuses = new ObjectStatusData[nR.ReadInt16()];
            for (int i = 0; i < Statuses.Length; i++)
            {
                Statuses[i] = new ObjectStatusData();
                Statuses[i].Read(nR);
            }
        }

        public override void Write(NWriter nW)
        {
            throw new NotImplementedException();
        }
    }
}
