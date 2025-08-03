using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class CopyBoxColliderBounds : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] BoxCollider2D sourceCollider;

    void Start()
    {
        transform.position = sourceCollider.bounds.center;
        transform.localScale = new Vector3(sourceCollider.bounds.size.x, sourceCollider.bounds.size.y, 1f);
        transform.rotation = Quaternion.Euler(0f, 0f, sourceCollider.transform.eulerAngles.z);
    }
}
