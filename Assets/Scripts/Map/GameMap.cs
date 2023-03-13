using RotmgClient.Map;
using RotmgClient.Networking.Data;
using RotmgClient.Util;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    public static GameMap Instance;

    private Stack<GroundTile> groundTiles;

    private Stack<GroundTileData> newGroundTiles;

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
        GroundLibrary.loadSprites();
        AssetLibrary.AddSpriteSet("lofiEnvironment2", "Sprites/lofiEnvironment2");
        newGroundTiles = new Stack<GroundTileData>();
        groundTiles = new Stack<GroundTile>();
        var text = Resources.Load<TextAsset>("Data/GroundCXML");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text.text);
        GroundLibrary.ParseFromXML(doc);
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
    }


    public void AddTile(GroundTileData tile)
    {
        newGroundTiles.Push(tile);
    }
}
