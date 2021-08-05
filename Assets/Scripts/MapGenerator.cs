using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Net;

[System.Serializable]
public class MapItem {
    public int x;
    public int y;
    public int type;
}

[System.Serializable]
public class Map {
    public string name;
    public string owner;
    public MapItem[] map;
}

public class MapGenerator : MonoBehaviour {

    public GameObject[] blocks;
    public string mapName = "Test";

    // Start is called before the first frame update
    void Start() {
        getMap();
    }

    private void getMap() {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://localhost:6000/api/map/"+mapName);
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Map map = JsonUtility.FromJson<Map>(jsonResponse);

        BuildMap(map);
    }

    public void BuildMap(Map map) {
        foreach(MapItem mapItem in map.map) {
            Instantiate(blocks[mapItem.type - 1], new Vector3(mapItem.x, mapItem.y, 0), Quaternion.identity, transform);
        }
    }
}
