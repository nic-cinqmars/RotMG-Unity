namespace RotmgClient.Networking.Data
{
    public class ObjectStatusData
    {
        public int objectId;
        public WorldPosData pos;
        public StatData[] stats;

        public void Read(NReader nR)
        {
            objectId = nR.ReadInt32();

            pos = new WorldPosData();
            pos.Read(nR);

            stats = new StatData[nR.ReadByte()];
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = new StatData();
                stats[i].Read(nR);
            }
        }

        public void Write(NWriter nR)
        {
            nR.Write(objectId);

            pos.Write(nR);

            nR.Write((short)stats.Length);
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].Write(nR);
            }
        }
    }
}
