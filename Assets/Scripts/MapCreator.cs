using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject[] blocks;

    // Update is called once per frame
    void Update() {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
        if (hitData && Input.GetMouseButtonDown(0)) {
            Debug.Log(worldPosition);

            // Debug.Log(hitData.transform.gameObject.name);
            Vector3 pos = new Vector3(Mathf.Floor(worldPosition.x), Mathf.Floor(worldPosition.y), -1);
            Debug.Log(pos);
            Debug.Log(blocks[0].tag);
            Instantiate(blocks[0], pos, Quaternion.identity);
        }
    }
}

