using RotmgClient.Networking.Data;
using RotmgClient.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Map
{
    public interface IGameMap
    {
        void AddTile(GroundTileData tile);
        void AddObject(ObjectData objectData);
        void RemoveObject(int objectID);
        public RotmgGameObject GetGameObject(int objectId);

    }
}
