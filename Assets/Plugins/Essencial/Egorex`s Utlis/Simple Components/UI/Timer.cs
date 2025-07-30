using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    float time = 0;
    TextMeshProUGUI text;
    public bool run = true;

    void Awake(){
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update(){
        if (!run){
            return;
        }
        time += Time.deltaTime;
        text.text = GetTimeString();
    }
    public string GetTimeString(){
        return ConvertTimeToString(time);
    }
    public float GetTimeFloat(){
        return time;
    }
    public static string ConvertTimeToString(float time){
        var timeSpan = TimeSpan.FromSeconds(time);
        return timeSpan.ToString("mm':'ss':'f"); 
    }
}
