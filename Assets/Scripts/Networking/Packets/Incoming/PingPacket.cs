using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class PingPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.PING;

        public int serial;

        public override void Read(NReader pR)
        {
            serial = pR.ReadInt32();
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
