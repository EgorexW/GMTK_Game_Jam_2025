using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CopyBoxColliderBounds : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] BoxCollider2D sourceCollider;
    
    [GetComponent][SerializeField] BoxCollider2D targetCollider;

    void Start()
    {
        targetCollider.offset = sourceCollider.offset;
        targetCollider.size = sourceCollider.size;
    }
}
