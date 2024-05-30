using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// This annotation is needed/recommended in order to make Unity serialization work!
// Remember to follow the "Serialization Rules" which can be found at the top of this page:
// https://docs.unity3d.com/Manual/script-Serialization.html
[System.Serializable]
public class GameData
{
    public int integer;
    public float rational;
    public string text;
}

public class Connor
{
    public string coolness = "what a loser xD xD xD";
}

public class DataManager : MonoBehaviour
{
    GameData saveData = new GameData();
    GameData loadData = null;

    // Our game must be running before we specify our path (must put in Awake or Start).
    string path = null;

    void Start()
    {
        // Location of our save file. Arg1 is the file location, arg 2 is the name of the file.
        path = Path.Combine(Application.persistentDataPath, "GameData.json");
        //Debug.Log(path); <-- Uncomment to see where your data is being saved!

        Save();
        Load();
    }

    void Save()
    {
        // 1. Populate data
        saveData.integer = 1;
        saveData.rational = 2.2f;
        saveData.text = "gronk";

        // 2. Convert data to json (a file-format that Unity understands)
        string json = JsonUtility.ToJson(saveData);
        Debug.Log("Save data: " + json);

        // 3. Write json data to file!
        File.WriteAllText(path, json);
    }

    void Load()
    {
        if (File.Exists(path))
        {
            // 1. Read json data from file!
            string json = File.ReadAllText(path);

            // 2. Convert json to memory (GameData)
            loadData = JsonUtility.FromJson<GameData>(json);
            // Must specify object type within angle-bracks <> similar to GetComponent<>()!
            Debug.Log("Load data: " + json);
        }
    }

    void PlayerPrefsExample()
    {
        string key = "number";
        //PlayerPrefs.SetInt(key, 5);
        PlayerPrefs.DeleteKey(key);

        // Can associate a default value (69 in this case hahaaaaaaaa) if the key is not found
        int number = PlayerPrefs.GetInt(key, 69);
        Debug.Log(number);
    }
}
