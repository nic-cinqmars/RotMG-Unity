namespace RotmgClient.Networking.Packets
{
    public abstract class Packet
    {
        public abstract PacketId packetId { get; }

        public abstract void Crypt(byte[] data, int offset, int length);
        public abstract void Write(NWriter nW);
        public abstract void Read(NReader nR);
    }
}
