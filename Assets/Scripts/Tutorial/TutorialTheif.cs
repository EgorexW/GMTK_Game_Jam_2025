using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TutorialTheif : MonoBehaviour, IThiefAI
{
    [SerializeField] [GetComponent] AIPath aiPath;

    [BoxGroup("References")] [Required] [SerializeField] new Collider2D collider2D;

    [SerializeField] float moveSpeed = 4;

    [SerializeField] List<Transform> targetPoints;

    [FoldoutGroup("Events")] public UnityEvent onSurrender;

    int loopIndex;

    void Start()
    {
        Begin();
    }

    void Update()
    {
        if (aiPath.reachedDestination){
            ReachedDestination();
        }
    }

    public void Surrender()
    {
        aiPath.canMove = false;
        Debug.Log("Surrendered!", this);
        collider2D.enabled = false;
        onSurrender.Invoke();
    }

    public void Trap(float trapTime)
    {
        throw new NotImplementedException();
    }

    public Transform Transform => transform;
    public Node startNode{ get; } = null; // Not used in this tutorial, but required by the interface
    public RoundManager RoundManager{ get; set; } = null; // Not used in this tutorial, but required by the interface

    void Begin()
    {
        aiPath.maxSpeed = moveSpeed;
        ReachedDestination();
    }

    void ReachedDestination()
    {
        SelectNewDestination();
    }

    void SelectNewDestination()
    {
        var node = SelectNextNode();
        SetDestination(node.transform.position);
    }

    Transform SelectNextNode()
    {
        loopIndex++;
        if (loopIndex >= targetPoints.Count){
            loopIndex = 0;
        }
        return targetPoints[loopIndex];
    }

    void SetDestination(Vector2 destination)
    {
        aiPath.destination = destination;
    }
}