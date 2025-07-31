using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class TriggerLight : MonoBehaviour
{
    [SerializeField][GetComponent] Light2D lights;
    
    [SerializeField] GameObject triggerObject;

    void Awake()
    {
        lights.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerObject == other.attachedRigidbody.gameObject){
            lights.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerObject == other.attachedRigidbody.gameObject){
            lights.enabled = false;
        }
    }
}
