using RotmgClient.Map;
using RotmgClient.Networking.Data;
using RotmgClient.Objects;
using RotmgClient.Util;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    public static GameMap Instance;

    [SerializeField]
    private Transform gameObjectsTransform;

    private Stack<GroundTile> groundTiles;

    private Stack<GroundTileData> newGroundTiles;

    private Stack<ObjectData> newObjects;

    private Dictionary<int, RotmgGameObject> objectDict;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        AssetLoader assetLoader = new AssetLoader();
        assetLoader.Load();

        newGroundTiles = new Stack<GroundTileData>();
        newObjects = new Stack<ObjectData>();
        groundTiles = new Stack<GroundTile>();
        objectDict = new Dictionary<int, RotmgGameObject>();
    }

    void Update()
    {
        while (newGroundTiles.Count > 0)
        {
            GroundTileData tileData = newGroundTiles.Pop();
            GroundTile groundTile = new GroundTile(tileData.x, tileData.y);
            groundTile.SetTileType(tileData.type);

            GameObject gameObject = new GameObject(tileData.type.ToString());
            SpriteRenderer sprite = gameObject.AddComponent<SpriteRenderer>();
            sprite.sprite = groundTile.GetSprite();
            sprite.sortingOrder = -1;
            gameObject.transform.parent = transform;
            gameObject.transform.localPosition = new Vector2(tileData.x, tileData.y);
        }
        while (newObjects.Count > 0)
        {
            ObjectData objectData = newObjects.Pop();
            ushort objectType = objectData.objectType;
            GameObject gO = ObjectLibrary.GetObjectInstanceFromType(objectType);
            SpriteRenderer spriteRenderer = gO.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = ObjectLibrary.GetSpriteFromType(objectType);
            gO.transform.parent = gameObjectsTransform;
            gO.transform.localPosition = new Vector2(objectData.status.pos.x, objectData.status.pos.y);
            RotmgGameObject rotmgGameObject = gO.GetComponent<RotmgGameObject>();
            rotmgGameObject.AddTo(this, gO.transform.localPosition);
            objectDict.Add(objectData.status.objectId, rotmgGameObject);
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

    public RotmgGameObject GetGameObject(int objectId)
    {
        return objectDict[objectId];
    }
}
