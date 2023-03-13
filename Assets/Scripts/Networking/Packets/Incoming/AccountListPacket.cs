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
        public string[] AccountIds { get; set; }
        public int LockAction { get; set; }

        public override PacketId packetId => PacketId.ACCOUNTLIST;

        public override void Read(NReader pR)
        {
            AccountListId = pR.ReadInt32();
            AccountIds = new string[pR.ReadInt16()];
            for (int i = 0; i < AccountIds.Length; i++)
                AccountIds[i] = pR.ReadUTF();
            LockAction = pR.ReadInt32();
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
