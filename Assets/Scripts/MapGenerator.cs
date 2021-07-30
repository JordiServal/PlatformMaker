using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Net;

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

    public GameObject[] blocks;

    // Start is called before the first frame update
    void Start() {
        getMap();
    }

    private void getMap() {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://localhost:6000/api/map");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        JSONNode mapJson = reader.ReadToEnd();
        BuildMap(mapJson);
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
