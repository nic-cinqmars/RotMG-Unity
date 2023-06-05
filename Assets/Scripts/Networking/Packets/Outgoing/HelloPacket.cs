using RotmgClient.Cryptography;

namespace RotmgClient.Networking.Packets.Outgoing
{
    public class HelloPacket : OutgoingPacket
    {
        public string buildVersion;
        public int gameID;
        public string username;
        public string password;
        public string mapJSON;

        public override PacketId packetId => PacketId.Hello;

        public override void Write(NWriter pW)
        {
            pW.WriteUTF(buildVersion);
            pW.Write(gameID);
            pW.WriteUTF(username);
            pW.WriteUTF(password);
            pW.Write32UTF(mapJSON);
        }

        public override void Read(NReader pR)
        {
            // Throw exception
        }
    }

}
