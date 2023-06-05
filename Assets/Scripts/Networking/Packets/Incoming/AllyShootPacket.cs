using Org.BouncyCastle.Asn1.X500;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class AllyShootPacket : IncomingPacket
    {
        public byte BulletId { get; set; }
        public int OwnerId { get; set; }
        public ushort ContainerType { get; set; }
        public float Angle { get; set; }

        public override PacketId packetId => PacketId.AllyShoot;

        public override void Read(NReader nR)
        {
            BulletId = nR.ReadByte();
            OwnerId = nR.ReadInt32();
            ContainerType = nR.ReadUInt16();
            Angle = nR.ReadSingle();
        }

        public override void Write(NWriter nW)
        {
            throw new NotImplementedException();
        }
    }
}
