using RotmgClient.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Data
{
    public class ObjectDropData
    {
        public int objectId;
        public bool explode;

        public void Read(NReader nR)
        {
            objectId = nR.ReadInt32();
            explode = nR.ReadBoolean();
        }
    }
}
