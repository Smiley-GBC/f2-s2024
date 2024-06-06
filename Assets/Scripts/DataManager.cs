using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.VisualScripting.FullSerializer;
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

public class DataManager : MonoBehaviour
{
    GameData saveData = new GameData();
    GameData loadData = null;

    // Our game must be running before we specify our path (must put in Awake or Start).
    string pathJson = null;
    string pathXml = null;

    void Start()
    {
        // Location of our save file. Arg1 is the file location, arg 2 is the name of the file.
        pathJson = Path.Combine(Application.persistentDataPath, "GameData.json");
        pathXml = Path.Combine(Application.persistentDataPath, "GameData.xml");
        //Debug.Log(path); <-- Uncomment to see where your data is being saved!

        SaveJson();
        LoadJson();

        SaveXml();
        LoadXml();
    }

    void SaveJson()
    {
        // 1. Populate data
        saveData.integer = 1;
        saveData.rational = 2.2f;
        saveData.text = "gronk";

        // 2. Serialize (convert from memory to text)
        string json = JsonUtility.ToJson(saveData);
        Debug.Log("Save data: " + json);

        // 3. Write json data to file!
        File.WriteAllText(pathJson, json);
    }

    void LoadJson()
    {
        if (File.Exists(pathJson))
        {
            // 1. Read json data from file!
            string json = File.ReadAllText(pathJson);

            // 2. Deserialize (convert from text to memory)
            loadData = JsonUtility.FromJson<GameData>(json);

            // Must specify object type within angle-bracks <> similar to GetComponent<>()!
            Debug.Log("Load data: " + json);
        }
    }

    void SaveXml()
    {
        // 1. Populate data
        saveData.integer = 3;
        saveData.rational = 4.4f;
        saveData.text = "dronk";

        // 2. Create a serializer (mechanism to convert from memory to text)
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));

        // 3. Write the data to file using our serializer!
        using (StreamWriter streamWriter = new StreamWriter(pathXml))
        {
            serializer.Serialize(streamWriter, saveData);
        }
    }

    void LoadXml()
    {
        // 1. Create a (de)serializer (mechanism to convert from text to memory)
        XmlSerializer deserializer = new XmlSerializer(typeof(GameData));

        // 2. Read the data from file using our deserializer!
        using (StreamReader streamReader = new StreamReader(pathXml))
        {
            loadData = (GameData)deserializer.Deserialize(streamReader);
        }

        Debug.Log("Load data: " + loadData);
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
