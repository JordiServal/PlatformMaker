using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

[System.Serializable]
public class Column {
    public int[] rows;
}

[System.Serializable]
public class Map {
    public string name;
    public string owner;
    public int[,] columns;
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
            Debug.Log(json);
            JSONNode mapJson = JSON.Parse(json);
            Debug.Log(mapJson["name"]);
            Debug.Log(mapJson["columns"]);
            BuildMap(mapJson["columns"]);
        }
    }

    public void BuildMap(JSONNode columns) {
        int x = 0; 
        int y = 0;
        foreach(JSONNode column in columns) {
            y = 0;
            foreach(JSONNode cell in column) {
                if(cell != 0) {
                    Instantiate(blocks[0], new Vector3(x, y, 0), Quaternion.identity);
                }
                y++;
            } 
            x++;
        }
    }
}
