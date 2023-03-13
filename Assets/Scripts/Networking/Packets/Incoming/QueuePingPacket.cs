using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class QueuePingPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.QUEUE_PING;

        public int serial;
        public int position;
        public int count;

        public override void Read(NReader pR)
        {
            serial = pR.ReadInt32();
            position = pR.ReadInt32();
            count = pR.ReadInt32();
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
