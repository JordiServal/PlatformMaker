using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static float maxScoreValue = 0;
    public static float scoreValue = 0;
    public static int deathsValue = 0;
    
    Text score;

    void Start() {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if(maxScoreValue < scoreValue) maxScoreValue = scoreValue;

        score.text = "MaxScore: " + Mathf.Floor(maxScoreValue)+ "\n\r" + "Score: " + Mathf.Floor(scoreValue)+ "\n\r" + "Deaths: "+ deathsValue;
    }
}
