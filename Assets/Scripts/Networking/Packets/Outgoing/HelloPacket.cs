using RotmgClient.Cryptography;

namespace RotmgClient.Networking.Packets.Outgoing
{
    public class HelloPacket : OutgoingPacket
    {
        public string buildVersion;
        public int gameID;
        public string guid;
        public string password;
        public string secret;
        public int keyTime;
        public byte[] key;
        public string mapJSON;
        public string hash;

        public override PacketId packetId => PacketId.HELLO;

        public override void Write(NWriter pW)
        {
            pW.WriteUTF(buildVersion);
            pW.Write(gameID);
            pW.WriteUTF(RSA.getInstance().Encrypt(guid));
            pW.WriteUTF(RSA.getInstance().Encrypt(password));
            pW.WriteUTF(RSA.getInstance().Encrypt(secret));
            pW.Write(keyTime);
            pW.Write((short)key.Length);
            pW.Write(key);
            pW.Write32UTF(mapJSON);
            pW.WriteUTF(hash);
        }

        public override void Read(NReader pR)
        {
            // Throw exception
        }
    }

}
