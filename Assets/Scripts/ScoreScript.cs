using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static float scoreValue = 0;
    public static int deathsValue = 0;
    
    Text score;

    void Start() {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        score.text = "Score: " + Mathf.Floor(scoreValue)+ "\n\r" + "Deaths: "+ deathsValue;
    }
}
