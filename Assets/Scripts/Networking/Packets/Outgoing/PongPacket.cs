using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Outgoing
{
    public class PongPacket : OutgoingPacket
    {
        public override PacketId packetId => PacketId.PONG;
        public int Serial { get; set; }
        public int Time { get; set; }

        public override void Read(NReader pR)
        {
            throw new NotImplementedException();
        }

        public override void Write(NWriter pW)
        {
            pW.Write(Serial);
            pW.Write(Time);
        }
    }
}
