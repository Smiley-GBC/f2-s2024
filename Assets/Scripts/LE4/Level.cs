using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    List<List<GameObject>> tileObjects = new List<List<GameObject>>();

    const int rowCount = 10;
    const int colCount = 20;

    void Start()
    {
        float x = 0.5f;
        float y = 10.0f - 0.5f;

        for (int i = 0; i < rowCount; i++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0;  j < colCount; j++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x + j, y);
                row.Add(tile);
            }
            tileObjects.Add(row);
            y -= 1.0f;
        }
    }

    void Update()
    {
        Gradient();
    }

    void Gradient()
    {
        for (int row = 0; row < rowCount; row++)
        {
            float red = row / (float)rowCount;
            for (int col = 0; col < colCount; col++)
            {
                float green = col / (float)colCount;
                Color color = new Color(red, green, 0.0f);
                tileObjects[row][col].GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}
