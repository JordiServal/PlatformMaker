using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    // Update is called once per frame
    void Update() {
        if(target != null){

            transform.position = new Vector3(
                target.position.x,
                target.position.y,
                transform.position.z);

            // // Smooth camera movement
            // float posX = Mathf.Round( Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, delayTime) * 100) / 100;
            // float posY = Mathf.Round(Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref velocity.y, delayTime) * 100) / 100;

            // transform.position = new Vector3(
            //     Mathf.Clamp(posX, tLX, bRX),
            //     Mathf.Clamp(posY, bRY, tLY),
            //     transform.position.z);
        } 
    }

    public void SetTarget(Transform obj) {
        target = obj;
    }
}
