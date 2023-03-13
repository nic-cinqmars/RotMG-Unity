using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class FailurePacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.FAILURE;

        public const int INCORRECT_VERSION = 4;
        public const int BAD_KEY = 5;
        public const int INVALID_TELEPORT_TARGET = 6;
        public const int EMAIL_VERIFICATION_NEEDED = 7;
        public const int JSON_DIALOG = 8;

        public int ErrorId { get; set; }
        public string ErrorDescription { get; set; }

        public override void Read(NReader nR)
        {
            ErrorId = nR.ReadInt32();
            ErrorDescription = nR.ReadUTF();
        }

        public override void Write(NWriter nW)
        {
            throw new NotImplementedException();
        }
    }
}
