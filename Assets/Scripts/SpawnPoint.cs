using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update    
    void Start() {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.SetSpawnPoint(transform.position);
        player.Respawn();
    }
}
