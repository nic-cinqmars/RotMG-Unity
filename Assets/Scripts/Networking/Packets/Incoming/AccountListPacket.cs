using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class AccountListPacket : IncomingPacket
    {
        public int AccountListId { get; set; }
        public int[] AccountIds { get; set; }

        public override PacketId packetId => PacketId.AccountList;

        public override void Read(NReader pR)
        {
            AccountListId = pR.ReadInt32();
            AccountIds = new int[pR.ReadInt16()];
            for (int i = 0; i < AccountIds.Length; i++)
                AccountIds[i] = pR.ReadInt32();
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
