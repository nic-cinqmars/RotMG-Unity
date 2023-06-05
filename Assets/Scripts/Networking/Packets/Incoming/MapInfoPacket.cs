using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class MapInfoPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.MapInfo;

        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public uint Seed { get; set; }
        public int Background { get; set; }
        public bool ShowDisplays { get; set; }
        public bool AllowPlayerTeleport { get; set; }

        public override void Read(NReader pR)
        {
            Width = pR.ReadInt32();
            Height = pR.ReadInt32();
            Name = pR.ReadUTF();
            DisplayName = pR.ReadUTF();
            Seed = pR.ReadUInt32();
            Background = pR.ReadInt32();
            ShowDisplays = pR.ReadBoolean();
            AllowPlayerTeleport = pR.ReadBoolean();
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
