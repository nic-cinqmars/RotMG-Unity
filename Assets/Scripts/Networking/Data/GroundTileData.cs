using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Data
{
    public class GroundTileData
    {
        public short x;
        public short y;
        public ushort type;

        public void Read(NReader nR)
        {
            x = nR.ReadInt16();
            y = nR.ReadInt16();
            type = nR.ReadUInt16();
        }

        public void Write(NWriter nW)
        {
            nW.Write(x);
            nW.Write(y);
            nW.Write(type);
        }
    }
}
