using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Outgoing
{
    internal class PlayerText : OutgoingPacket
    {
        public string Text { get; set; }

        public override PacketId packetId => PacketId.PLAYERTEXT;

        public override void Read(NReader nR)
        {
            throw new NotImplementedException();
        }

        public override void Write(NWriter nW)
        {
            nW.WriteUTF(Text);
        }
    }
}
