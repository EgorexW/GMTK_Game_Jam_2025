using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShowLine : MonoBehaviour
{
    [GetComponent][SerializeField] LineRenderer lineRenderer;

    [SerializeField] List<Transform> objects;
    
    void Update()
    {
        lineRenderer.positionCount = objects.Count;
        foreach (var obj in objects){
            lineRenderer.SetPosition(objects.IndexOf(obj), obj.position);       
        }
    }
}
