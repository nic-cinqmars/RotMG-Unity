using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Packets.Incoming
{
    public class MapInfoPacket : IncomingPacket
    {
        public override PacketId packetId => PacketId.MAPINFO;

        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Difficulty { get; set; }
        public uint Seed { get; set; }
        public int Background { get; set; }
        public bool AllowPlayerTeleport { get; set; }
        public bool ShowDisplays { get; set; }
        public string[] ClientXML { get; set; }
        public string[] ExtraXML { get; set; }
        public string Music { get; set; }

        public override void Read(NReader pR)
        {
            Width = pR.ReadInt32();
            Height = pR.ReadInt32();
            Name = pR.ReadUTF();
            DisplayName = pR.ReadUTF();
            Seed = pR.ReadUInt32();
            Background = pR.ReadInt32();
            Difficulty = pR.ReadInt32();
            AllowPlayerTeleport = pR.ReadBoolean();
            ShowDisplays = pR.ReadBoolean();

            ClientXML = new string[pR.ReadInt16()];
            for (int i = 0; i < ClientXML.Length; i++)
                ClientXML[i] = pR.ReadUTF32();

            ExtraXML = new string[pR.ReadInt16()];
            for (int i = 0; i < ExtraXML.Length; i++)
                ExtraXML[i] = pR.ReadUTF32();

            Music = pR.ReadUTF();
        }

        public override void Write(NWriter pW)
        {
            throw new NotImplementedException();
        }
    }
}
