using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    void Start()
    {
        string key = "number";
        //PlayerPrefs.SetInt(key, 5);
        PlayerPrefs.DeleteKey(key);

        // Can associate a default value (69 in this case hahaaaaaaaa) if the key is not found
        int number = PlayerPrefs.GetInt(key, 69);
        Debug.Log(number);
    }

    void Update()
    {
        
    }
}
