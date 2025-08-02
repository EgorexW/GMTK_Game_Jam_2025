using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool run = true;
    TextMeshProUGUI text;
    float time;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!run){
            return;
        }
        time += Time.deltaTime;
        text.text = GetTimeString();
    }

    public string GetTimeString()
    {
        return ConvertTimeToString(time);
    }

    public float GetTimeFloat()
    {
        return time;
    }

    public static string ConvertTimeToString(float time)
    {
        var timeSpan = TimeSpan.FromSeconds(time);
        return timeSpan.ToString("mm':'ss':'f");
    }
}