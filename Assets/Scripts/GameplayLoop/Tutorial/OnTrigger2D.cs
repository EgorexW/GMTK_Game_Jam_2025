using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger2D : MonoBehaviour
{
    public UnityEvent<Collider2D> onTrigger;
    
    [SerializeField] List<GameObject> triggerObjects;
    
    [SerializeField] bool triggerOnEnter = true;
    [SerializeField] bool triggerOnStay = false;
    [SerializeField] bool triggerOnExit = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggerOnEnter){
            return;
        }
        if (!triggerObjects.Contains(other.gameObject)){
            return;
        }
        onTrigger.Invoke(other);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!triggerOnStay){
            return;
        }
        if (!triggerObjects.Contains(other.gameObject)){
            return;
        }
        onTrigger.Invoke(other);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!triggerOnExit){
            return;
        }
        if (!triggerObjects.Contains(other.gameObject)){
            return;
        }
        onTrigger.Invoke(other);
    }
}
