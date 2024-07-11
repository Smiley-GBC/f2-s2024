using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum TileType
{
    INVALID = -1,
    AIR,
    WALL
}

public class Level : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    List<List<GameObject>> tileObjects = new List<List<GameObject>>();

    const int rowCount = 10;
    const int colCount = 20;

    int[,] tileTypes = new int[rowCount, colCount];
    //{
    //    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
    //    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    //};

    void Start()
    {
        using (StreamReader reader = new StreamReader("Assets/Levels/Level1.txt"))
        {
            for (int row = 0; row < rowCount; row++)
            {
                string line = reader.ReadLine();
                for (int col = 0; col < colCount; col++)
                {
                    // TODO -- remove spaces from line (space = 32 in ascii, so we get -16 when we subtract '0')
                    int value = line[col] - '0';
                    tileTypes[row, col] = value;
                }
            }
        }

        float x = 0.5f;
        float y = 10.0f - 0.5f;
        
        for (int row = 0; row < rowCount; row++)
        {
            List<GameObject> tileObjectRow = new List<GameObject>();
            for (int col = 0; col < colCount; col++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x + col, y);

                // Set tile to invalid if its a negative number
                TileType type = tileTypes[row, col] >= 0 ?
                    (TileType)tileTypes[row, col] : TileType.INVALID;

                Color color = TileColor(type);
                bool collision = TileCollision(type);
                tile.GetComponent<Collider2D>().enabled = collision;
                tile.GetComponent<SpriteRenderer>().color = color;
        
                tileObjectRow.Add(tile);
            }
            tileObjects.Add(tileObjectRow);
            y -= 1.0f;
        }
    }

    void Update()
    {
        // Uncomment for fancy colours
        //Gradient();
    }

    void Gradient()
    {
        float tt = Time.realtimeSinceStartup;
        for (int row = 0; row < rowCount; row++)
        {
            float red = row / (float)rowCount;
            for (int col = 0; col < colCount; col++)
            {
                float green = col / (float)colCount;
                Color color = new Color(red, green, Mathf.Cos(tt) * 0.5f + 0.5f);
                tileObjects[row][col].GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    Color TileColor(TileType type)
    {
        switch (type)
        {
            case TileType.AIR: return Color.grey;
            case TileType.WALL: return Color.black;
            case TileType.INVALID: return Color.magenta;
        }
        return Color.white;
    }

    bool TileCollision(TileType type)
    {
        switch (type)
        {
            case TileType.AIR: return false;
            case TileType.WALL: return true;
            case TileType.INVALID: return false;
        }
        return false;
    }
}
