using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class GlobalNotificationPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.Notification;

        public const int ADD_ARENA = 1;
        public const int DELETE_ARENA = 2;

        public int Type { get; set; }
        public string Text { get; set; }

        public override void Read(NReader nR)
        {
            Type = nR.ReadInt32();
            Text = nR.ReadUTF();
        }

        public override void Write(NWriter nW)
        {
            throw new NotImplementedException();
        }
    }
}
