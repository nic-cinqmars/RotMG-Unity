using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Data
{
    public class WorldPosData
    {
        public float x;
        public float y;

        public void Read(NReader nR)
        {
            x = nR.ReadSingle();
            y = nR.ReadSingle();
        }

        public void Write(NWriter nR)
        {
            nR.Write(x);
            nR.Write(y);
        }
    }
}
