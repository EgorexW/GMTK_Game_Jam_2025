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
        if (boxNode.On){
            return;
        }
        if (Time.time - lastTriggerTime < triggerDelay){
            return;
        }
        lastTriggerTime = Time.time;
        ShowNoise();
    }
}