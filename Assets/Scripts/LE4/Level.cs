using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    List<List<GameObject>> tileObjects = new List<List<GameObject>>();

    const int rowCount = 10;
    const int colCount = 20;

    int[,] tileTypes =
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };

    void Start()
    {
        using (StreamReader reader = new StreamReader("Assets/Levels/Level1.txt"))
        {
            string line = reader.ReadLine();
            Debug.Log(line);
        }

        //float x = 0.5f;
        //float y = 10.0f - 0.5f;
        //
        //for (int row = 0; row < rowCount; row++)
        //{
        //    List<GameObject> tileObjectRow = new List<GameObject>();
        //    for (int col = 0; col < colCount; col++)
        //    {
        //        GameObject tile = Instantiate(tilePrefab);
        //        tile.transform.position = new Vector3(x + col, y);
        //
        //        int value = tileTypes[row, col];
        //        Color color = value == 0 ? Color.gray : Color.white;
        //        bool collision = value != 0;
        //        tile.GetComponent<Collider2D>().enabled = collision;
        //        tile.GetComponent<SpriteRenderer>().color = color;
        //
        //        tileObjectRow.Add(tile);
        //    }
        //    tileObjects.Add(tileObjectRow);
        //    y -= 1.0f;
        //}
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
}
