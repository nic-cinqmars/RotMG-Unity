using RotmgClient.Map;
using RotmgClient.Networking;
using RotmgClient.Networking.Packets.Incoming;
using RotmgClient.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotmgClient.Game
{
    public class GameManager : MonoBehaviour
    {
        private GameMap gameMap;
        public IGameMap map;

        private Client client;

        void Awake()
        {
            AssetLoader assetLoader = new AssetLoader();
            assetLoader.Load();

            GameObject gameMapObject = new GameObject("GameMap");
            gameMapObject.transform.parent = transform.parent;
            gameMap = gameMapObject.AddComponent<GameMap>();
            map = gameMap;

            GameObject clientObject = new GameObject("Client");
            clientObject.transform.parent = transform.parent;
            client = clientObject.AddComponent<Client>();
            client.Initialize(this, -1, false, 0, null);
        }

        public void ApplyMapInfo(MapInfoPacket mapInfo)
        {
            gameMap.SetProperties(mapInfo.Width, mapInfo.Height, mapInfo.Name, mapInfo.Background, mapInfo.AllowPlayerTeleport, mapInfo.ShowDisplays);
        }
    }
}
