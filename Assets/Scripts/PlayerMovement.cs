using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerController controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    private void Awake() {
        Camera.main.GetComponent<CameraController>().SetTarget(gameObject.transform);
    }

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if(Input.GetKeyDown(KeyCode.R)){
            controller.Respawn();
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
