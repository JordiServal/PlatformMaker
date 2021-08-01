using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {
    Text timeText;

    public float runTime;
    public float recordTime = -1f;
    public bool record = true;

    void Start() {
        timeText = GetComponent<Text>();
        RestartTimer();
    }

    // Update is called once per frame
    void Update() {
        if(record) {
            PrintTime();
        }
    }

    void PrintTime() {
        float currentTime = Time.time - runTime;
        timeText.text = FormatTime(currentTime);
        if (recordTime > 0) 
            timeText.text += "\n\rRecord: " + FormatTime(recordTime);
    }

    public string FormatTime( float time ) {
        int minutes = (int) time / 60 ;
        int seconds = (int) time - 60 * minutes;
        int milliseconds = (int) (1000 * (time - minutes * 60 - seconds));
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds );
    }


    public void RestartTimer() {
        runTime = Time.time;
        record = true;
    }

    public void StopTimer() {
        record = false;
        if(recordTime > Time.time - runTime || recordTime == 0)
            recordTime = Time.time - runTime;

        PrintTime();
    }
}
