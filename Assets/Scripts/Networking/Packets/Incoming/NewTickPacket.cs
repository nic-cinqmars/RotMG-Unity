using RotmgClient.Networking.Data;
using System;
using System.Net.Sockets;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class NewTickPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.NewTick;
        public ObjectStatusData[] Statuses { get; set; }
        public StatData[] PlayerStats { get; set; }

        public override void Read(NReader nR)
        {
            Statuses = new ObjectStatusData[nR.ReadInt16()];
            for (int i = 0; i < Statuses.Length; i++)
            {
                Statuses[i] = new ObjectStatusData();
                Statuses[i].Read(nR);
            }

            if (nR.BaseStream.Position != nR.BaseStream.Length)
            {
                PlayerStats = new StatData[nR.ReadByte()];
                for (int i = 0; i < PlayerStats.Length; i++)
                {
                    PlayerStats[i] = new StatData();
                    PlayerStats[i].Read(nR);
                }
            }
        }

        public override void Write(NWriter nW)
        {
            throw new NotImplementedException();
        }
    }
}
