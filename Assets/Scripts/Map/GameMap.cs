using RotmgClient.Map;
using RotmgClient.Networking.Data;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
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
        newGroundTiles = new Stack<GroundTileData>();
        groundTiles = new Stack<GroundTile>();
        var text = Resources.Load<TextAsset>("Data/GroundCXML");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text.text);
        GroundLibrary.parseFromXML(doc);
    }

    void Update()
    {
        while (newGroundTiles.Count > 0)
        {
            GroundTileData tile = newGroundTiles.Pop();
            GroundTile groundTile = new GroundTile(tile.x, tile.y);
            groundTile.setTileType(tile.type);

            GameObject gameObject = new GameObject(tile.type.ToString());
            SpriteRenderer sprite = gameObject.AddComponent<SpriteRenderer>();
            if (GroundLibrary.groundSpriteLibrary.TryGetValue(tile.type, out Sprite newSprite))
            {
                sprite.sprite = newSprite;
            }
            else
            {
                sprite.sprite = Resources.Load<Sprite>("Sprites/UnsetTexture");
            }
            gameObject.transform.parent = transform;
            gameObject.transform.localPosition = new Vector2(tile.x, tile.y);
        }
    }


    public void AddTile(GroundTileData tile)
    {
        newGroundTiles.Push(tile);
    }
}
