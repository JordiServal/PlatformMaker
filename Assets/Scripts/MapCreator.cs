using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject[] blocks;

    // Update is called once per frame
    void Update() {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hitData = Physics2D.RaycastAll(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
        if (hitData.Length > 0 && Input.GetMouseButtonDown(0)) {
			foreach (RaycastHit2D hit in hitData) {
                if (hit.transform.gameObject.layer ==  6) {
                    Destroy(hit.transform.parent.gameObject);
                }
            }
            Vector3 pos = new Vector3((int) worldPosition.x, (int) worldPosition.y, -1);
            // Debug.Log(pos);
            Instantiate(blocks[0], pos, Quaternion.identity);
        }
    }
}

