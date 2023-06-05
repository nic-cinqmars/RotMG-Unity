using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Outgoing
{
    public class LoadPacket : OutgoingPacket
    {
        public int CharId { get; set; }

        public override PacketId packetId => PacketId.Load;

        public override void Read(NReader pR)
        {
            throw new NotImplementedException();
        }

        public override void Write(NWriter pW)
        {
            pW.Write(CharId);
        }
    }
}
