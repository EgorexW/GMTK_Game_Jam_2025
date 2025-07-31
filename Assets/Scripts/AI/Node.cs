using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Node : MonoBehaviour
{
    [FormerlySerializedAs("forwardNode")] [FormerlySerializedAs("nextMainNode")] [FormerlySerializedAs("nextNodes")]
    public List<Node> forwardNodes;
    public List<Node> sideNodes;
    public List<Node> backwardNodes;
    
    public bool gemNode = false;
    public bool interactable;
    [ShowIf("interactable")] public UnityEvent onInteract;
    public bool HasSideNodes => sideNodes.Count > 0;

    void Awake()
    {
        foreach (var node in forwardNodes){
            node.backwardNodes.Add(this);
        }
    }

    public void Interact()
    {
        onInteract?.Invoke();
    }

    public Node GetForwardNode()
    {
        return forwardNodes.Random();
    }
    public Node GetSideNode()
    {
        return sideNodes.Count == 0 ? GetForwardNode() : sideNodes.Random();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Node node in forwardNodes)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
        Gizmos.color = Color.blue;
        foreach (Node node in sideNodes)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

}