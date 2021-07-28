using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// [Serializable]
public class Column {
    public int[] rows;
}

// [Serializable]
public class Map {
    public string name;
    public string owner;
    public Column[] columns;
}

public class MapGenerator : MonoBehaviour {
    private string mapFile = "map.json";

    public GameObject[] blocks;

    // Start is called before the first frame update
    void Start() {
        string fileName = Path.Combine(Application.dataPath, mapFile);
        LoadJson(fileName);
        
    }
    public void LoadJson(string fileName)
    {
        using (StreamReader r = new StreamReader(fileName))
        {
            string json = r.ReadToEnd();
            Map items = JsonUtility.FromJson<Map>(json);
            for (int i = 0; i < items.columns.Length; i++)
            {

            }
        }
    }
}
