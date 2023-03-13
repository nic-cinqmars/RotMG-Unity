using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Data
{
    public class ObjectData
    {
        public ushort objectType;
        public ObjectStatusData status;

        public void Read(NReader nR)
        {
            objectType = nR.ReadUInt16();
            status = new ObjectStatusData();
            status.Read(nR);
        }
    }
}
