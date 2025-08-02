using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class BoxNoiseEffect : NoiseEffect
{
    [BoxGroup("References")][Required][SerializeField] Box boxNode;
    
    [SerializeField] float triggerDelay = 5f;
    
    float lastTriggerTime;

    void Update()
    {
        if (Time.time - lastTriggerTime < triggerDelay){
            return;
        }
        lastTriggerTime = Time.time;
        if (boxNode.On){
            return;
        }
        ShowNoise();
    }
}