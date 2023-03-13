using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets
{
    public class PacketBuffer
    {
        public int index = 0;
        public byte[] bytes;

        public PacketBuffer()
        {
            bytes = new byte[4];
        }

        public void Resize(int newSize)
        {
            if (newSize > 1048576)
                throw new ArgumentException("New buffer size is too large");

            byte[] old = bytes;
            bytes = new byte[newSize];
            bytes[0] = old[0];
            bytes[1] = old[1];
            bytes[2] = old[2];
            bytes[3] = old[3];
        }

        public void Advance(int numBytes)
        {
            index += numBytes;
        }

        public void Reset()
        {
            bytes = new byte[4];
            index = 0;
        }

        public int BytesRemaining()
        {
            return bytes.Length - index;
        }

        public void Dispose()
        {
            bytes = null;
        }
    }
}
