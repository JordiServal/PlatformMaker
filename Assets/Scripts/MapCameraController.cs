using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour
{

    public float speed = 5f;
    
    float horizontalMove = 0f;
    float verticalMove = 0f;

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
    }

    void FixedUpdate() {
        transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y + verticalMove, transform.position.z);
    }
}
