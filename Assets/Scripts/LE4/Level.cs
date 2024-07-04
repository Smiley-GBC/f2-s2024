using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    List<GameObject> row = new List<GameObject>();

    void Start()
    {
        float x = 0.5f;
        float y = 0.5f;
        for (int i = 0; i < 20; i++)
        {
            GameObject tile = Instantiate(tilePrefab);
            tile.transform.position = new Vector3(x + i, y);
            row.Add(tile);
        }
    }

    void Update()
    {
        
    }
}
