using RotmgClient.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Outgoing
{
    public abstract class OutgoingPacket : Packet
    {
        public override void Crypt(byte[] data, int offset, int length)
        {
            RC4.getInstance().CryptSend(data, offset, length);
        }
    }
}
