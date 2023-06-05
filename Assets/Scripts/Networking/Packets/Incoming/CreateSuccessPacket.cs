using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class CreateSuccessPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.CreateSuccess;

        public int ObjectId { get; set; }
        public int CharId { get; set; }

        public override void Read(NReader nR)
        {
            ObjectId = nR.ReadInt32();
            CharId = nR.ReadInt32();
        }

        public override void Write(NWriter nW)
        {
            throw new NotImplementedException();
        }
    }
}
