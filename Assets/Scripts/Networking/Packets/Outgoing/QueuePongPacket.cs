using System;

namespace RotmgClient.Networking.Packets.Outgoing
{
    public class QueuePongPacket : OutgoingPacket
    {
        public override PacketId packetId => PacketId.QUEUE_PONG;

        public int serial;
        public int time;

        public override void Read(NReader pR)
        {
            throw new NotImplementedException();
        }

        public override void Write(NWriter pW)
        {
            pW.Write(serial);
            pW.Write(time);
        }
    }
}
