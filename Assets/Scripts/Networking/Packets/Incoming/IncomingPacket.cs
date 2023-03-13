using RotmgClient.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public abstract class IncomingPacket : Packet
    {
        public override void Crypt(byte[] data, int offset, int length)
        {
            RC4.getInstance().CryptRecieve(data, offset, length);
        }
    }
}
