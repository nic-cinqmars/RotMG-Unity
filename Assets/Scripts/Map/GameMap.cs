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

    private Stack<GroundTile> groundTiles;

    private Stack<GroundTileData> newGroundTiles;

    public List<Sprite> sprites = new List<Sprite>();

    private Stack<ObjectData> newObjects;

    private Dictionary<int, GameObject> objectDict;

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

        Texture2D texture2D = Resources.Load<Texture2D>("Sprites/lofiChar");
        for (int y = 1; y < texture2D.height / 8; y++)
        {
            for (int x = 0; x < texture2D.width / 8; x++)
            {   
                Rect rect = new Rect(x * 8, texture2D.height - (y * 8), 8, 8);
                Sprite sprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f), 8);
                sprites.Add(Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f)));
            }
        }

        newGroundTiles = new Stack<GroundTileData>();
        newObjects = new Stack<ObjectData>();
        groundTiles = new Stack<GroundTile>();
        objectDict = new Dictionary<int, GameObject>();
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
            gO.transform.parent = transform;
            gO.transform.localPosition = new Vector2(objectData.status.pos.x, objectData.status.pos.y);
            objectDict.Add(objectData.status.objectId, gO);
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
}
