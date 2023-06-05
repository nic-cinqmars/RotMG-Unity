using RotmgClient.Map;
using RotmgClient.Networking.Data;
using RotmgClient.Objects;
using RotmgClient.Util;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GameMap : MonoBehaviour, IGameMap
{
    private GameObject tilesGameObject;
    private GameObject rotmgObjectsGameObject;

    private Stack<GroundTile> groundTiles;

    private Stack<GroundTileData> newGroundTiles;

    private Stack<ObjectData> newObjects;

    private Stack<int> idsToRemove;

    private Dictionary<int, RotmgGameObject> objectDict;

    private Player player;

    // Map properties
    private int width;
    private int height;
    private string mapName;
    private int background;
    private bool allowPlayerTeleport;
    private bool showDisplays;

    void Start()
    {
        GameObject mapGameObject = new GameObject("Map");
        tilesGameObject = new GameObject("Tiles");
        tilesGameObject.transform.parent = mapGameObject.transform;
        rotmgObjectsGameObject = new GameObject("Objects");
        rotmgObjectsGameObject.transform.parent = mapGameObject.transform;

        player = null;

        newGroundTiles = new Stack<GroundTileData>();
        newObjects = new Stack<ObjectData>();
        idsToRemove = new Stack<int>();
        groundTiles = new Stack<GroundTile>();
        objectDict = new Dictionary<int, RotmgGameObject>();
    }

    public void SetProperties(int width, int height, string mapName, int background, bool allowPlayerTeleport, bool showDisplays)
    {
        this.width = width;
        this.height = height;
        this.mapName = mapName;
        this.background = background;
        this.allowPlayerTeleport = allowPlayerTeleport;
        this.showDisplays = showDisplays;
    }

    void Update()
    {
        while (newGroundTiles.Count > 0)
        {
            GroundTileData tileData = newGroundTiles.Pop();
            GroundTile groundTile = new GroundTile(tileData.x, tileData.y);
            groundTile.SetTileType(tileData.type);

            GameObject gameObject = new GameObject(tileData.type.ToString());
            gameObject.transform.parent = tilesGameObject.transform;
            gameObject.transform.localPosition = new Vector2(tileData.x, tileData.y);

            SpriteRenderer sprite = gameObject.AddComponent<SpriteRenderer>();
            sprite.sprite = groundTile.GetSprite();
            sprite.sortingOrder = -1;
        }
        while (newObjects.Count > 0)
        {
            ObjectData objectData = newObjects.Pop();
            ushort objectType = objectData.objectType;

            GameObject gO = ObjectLibrary.GetObjectInstanceFromType(objectType);
            gO.transform.parent = rotmgObjectsGameObject.transform;
            gO.transform.localPosition = new Vector2(objectData.status.pos.x, objectData.status.pos.y);

            RotmgGameObject rotmgGameObject = gO.GetComponent<RotmgGameObject>();
            rotmgGameObject.AddTo(this, gO.transform.localPosition);
            objectDict.Add(objectData.status.objectId, rotmgGameObject);
        }

        while (idsToRemove.Count > 0)
        {
            int objectID = idsToRemove.Pop();

            RotmgGameObject rotmgGameObject = objectDict[objectID];
            Destroy(rotmgGameObject.gameObject);

            objectDict.Remove(objectID);
        }

        foreach (RotmgGameObject rGO in objectDict.Values)
        {
            rGO.Draw();
        }
    }


    public void AddTile(GroundTileData tile)
    {
        newGroundTiles.Push(tile);
    }

    public void AddObject(ObjectData objectData)
    {
        newObjects.Push(objectData);
    }

    public void RemoveObject(int objectID)
    {
        idsToRemove.Push(objectID);
    }

    public RotmgGameObject GetGameObject(int objectId)
    {
        return objectDict[objectId];
    }
}
