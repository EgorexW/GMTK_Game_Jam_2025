using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Node : MonoBehaviour
{
    [FormerlySerializedAs("forwardNode")] [FormerlySerializedAs("nextMainNode")] [FormerlySerializedAs("nextNodes")]
    public List<Node> forwardNodes;
    public List<Node> sideNodes;
    public List<Node> interactiveNodes;
    public List<Node> backwardNodes;
    
    public bool gemNode = false;
    public bool active = true;
    
    protected virtual bool IsInteractive => false;
    [ShowIf("IsInteractive")] public UnityEvent onInteract;

    protected virtual void Awake()
    {
        foreach (var node in forwardNodes){
            node.backwardNodes.Add(this);
        }
        foreach (var sideNode in sideNodes.Copy().Where(sideNode => sideNode.IsInteractive)){
            interactiveNodes.Add(sideNode);
            sideNodes.Remove(sideNode);
        }
    }
    public void Interact()
    {
        onInteract?.Invoke();
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
        Gizmos.color = Color.green;
        foreach (Node node in interactiveNodes)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    public Node GetRandomForwardNode()
    {
        return forwardNodes.Random();
    }

    public Node GetRandomSideNode()
    {
        return sideNodes.Count == 0 ? null : sideNodes.Random();
    }

    public Node GetRandomInteractiveNode()
    {
        var pool = interactiveNodes.Where(node => node.active).ToList();
        return pool.Count == 0 ? null : pool.Random();
    }
}