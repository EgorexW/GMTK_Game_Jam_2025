using System;
using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ThiefAI : MonoBehaviour, INoiseHearer
{
    [SerializeField][GetComponent] AIPath aiPath;
    
    [FormerlySerializedAs("gameManager")] [FormerlySerializedAs("ai")] [FormerlySerializedAs("nodes")] [BoxGroup("References")][Required][SerializeField] RoundManager roundManager;
    
    [SerializeField][BoxGroup("Config")] List<Transform> seePoints;
    
    [SerializeField] float waitTime = 4;
    [SerializeField] float runAwayTime = 2;
    [SerializeField] float seeDis = 20;
    [SerializeField] float movementSeeMod = 0.5f;
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float runSpeed = 8;
    [SerializeField] float leaveTime = 5;
    [SerializeField] int sideMovementsPerTry = 2;
    [SerializeField] float sideMovementChance = 0.5f;
    [SerializeField] float interactiveMovementChance = 0.5f;
    
    [FoldoutGroup("Debug")][ShowInInspector] bool hasGem = false;
    [FoldoutGroup("Debug")][ShowInInspector] Node startNode;
    [FoldoutGroup("Debug")][ShowInInspector] Node node;
    [FoldoutGroup("Debug")][ShowInInspector] float currentWaitTime = 0;
    [FoldoutGroup("Debug")][ShowInInspector] State state;
    [FoldoutGroup("Debug")][ShowInInspector] float guardSeenTime;
    [FoldoutGroup("Debug")][ShowInInspector] int sideMovementsLeft;
    [FoldoutGroup("Debug")][ShowInInspector] float trapTime;

    void Start()
    {
        Begin();
    }

    void Begin()
    {
        startNode = roundManager.GetStartNode();
        node = startNode;
        var position = startNode.transform.position;
        aiPath.Teleport(position);
        aiPath.destination = position;
        aiPath.maxSpeed = moveSpeed;
        sideMovementsLeft = sideMovementsPerTry;
        state = State.Moving;
        ReachedDestination();
    }

    void Update()
    {
        switch (state){
            case State.Trapped:{
                trapTime -= Time.deltaTime;
                if (trapTime <= 0){
                    aiPath.canMove = true;
                    currentWaitTime = 0;
                    RunAway();
                }
                return;
            }
            case State.Leaving:
                WaitForLeave();
                return;
            case State.Surrendered:
                return;
            case State.Escaping:{
                if (aiPath.reachedDestination){
                    ReachedDestination();
                }
                return;
            }
            case State.Waiting or State.Moving or State.MovingBackwards when LookForGuard():
                return;
            case State.Waiting:
                Wait();
                return;
            case State.Moving or State.MovingBackwards:{
                if (aiPath.reachedDestination){
                    ReachedDestination();
                }
                break;
            }
        }
    }

    void WaitForLeave()
    {
        currentWaitTime += Time.deltaTime;
        if (!(currentWaitTime >= leaveTime)){
            return;
        }
        if (hasGem){
            roundManager.GameLost();
        }
        aiPath.canMove = true;
        Begin();
    }

    void Wait()
    {
        currentWaitTime += Time.deltaTime;
        if (!(currentWaitTime >= waitTime)){
            return;
        }
        node.Interact();
        SelectNewDestination();
    }

    bool LookForGuard()
    {
        foreach (var point in seePoints){
            Vector2 targetDir = roundManager.guard.transform.position - point.position;
            var hit = Physics2D.Raycast(point.position, targetDir, seeDis);
            var componentFromCollider = General.GetComponentFromCollider<Transform>(hit.collider);
            // Debug.Log(componentFromCollider);
            if (componentFromCollider != roundManager.guard.transform){
                continue;
            }
            Debug.DrawRay(point.position, targetDir * seeDis, Color.yellow);
            GuardSpotted();
            return true;
        }
        return false;
    }

    void GuardSpotted()
    {
        var seeMod = state == State.Moving ? movementSeeMod : 1f;
        guardSeenTime += Time.deltaTime;
        if (guardSeenTime >= runAwayTime){
            RunAway();
        }
    }

    void RunAway()
    {
        node = SelectEscapeNode();
        state = State.Escaping;
        aiPath.maxSpeed = runSpeed;
        SetDestination(node.transform.position);
    }

    Node SelectEscapeNode()
    {
        var guardDir = (roundManager.guard.transform.position - transform.position).normalized;
        var smallestDot = float.MaxValue;
        Node escapeNode = null;
        foreach (var node in roundManager.startNodes){
            var nodeDir = (node.transform.position - transform.position).normalized;
            var dot = Vector2.Dot(guardDir, nodeDir);
            if (dot > smallestDot){
                continue;
            }
            smallestDot = dot;
            escapeNode = node;
        }
        return escapeNode;
    }

    void ReachedDestination()
    {
        if (node.gemNode){
            CollectGem();
            return;
        }
        if (state == State.Escaping){
            Leave();
            return;
        }
        state = State.Waiting;
        currentWaitTime = 0;
        guardSeenTime = 0;
    }

    void CollectGem()
    {
        node.Interact();
        hasGem = true;
        startNode = roundManager.GetStartNode();
        RunAway();
    }

    void Leave()
    {
        state = State.Leaving;
        aiPath.canMove = false;
        currentWaitTime = 0;
    }

    void SelectNewDestination()
    {
        node = SelectNextNode();
        state = State.Moving;
        SetDestination(node.transform.position);
    }

    Node SelectNextNode()
    {
        if (Random.value < interactiveMovementChance){
            var nodeTmp = node.GetRandomInteractiveNode();
            if (nodeTmp != null){
                return nodeTmp;
            }
        }
        if (sideMovementsLeft > 0 && Random.value < sideMovementChance){
            var nodeTmp = node.GetRandomSideNode();
            if (nodeTmp != null){
                sideMovementsLeft--;
                return nodeTmp;
            }
        }
        return node.GetRandomForwardNode();
    }

    void SetDestination(Vector2 destination)
    {
        aiPath.destination = destination;
    }

    public void Surrender()
    {
        aiPath.canMove = false;
        state = State.Surrendered;
        Debug.Log("Surrendered!", this);
        enabled = false;
        roundManager.TheifCaught(transform);
    }

    public void NoiseHeard(Vector2 position)
    {
        if (state is State.Surrendered or State.Leaving or State.Escaping or State.MovingBackwards){
            return;
        }
        MoveBackwards(position);
    }

    void MoveBackwards(Vector2 position)
    {
        var noiseDir = (position - (Vector2)transform.position).normalized;
        var choosenNode = node.GetRandomBackwardNode();
        foreach (var nodeTmp in node.backwardNodes){
            if (Vector2.Dot(noiseDir, (nodeTmp.transform.position - transform.position).normalized) < 0.5f){
                continue;
            }
            choosenNode = nodeTmp;
            break;
        }
        if (choosenNode == null){
            Debug.LogWarning("No backward node found!", this);
            return;
        }
        node = choosenNode;
        state = State.MovingBackwards;
        SetDestination(node.transform.position);
    }

    public void Trap(float trapTime)
    {
        state = State.Trapped;
        aiPath.canMove = false;
        this.trapTime = trapTime;
    }
}

enum State
{
    Moving,
    Waiting,
    Surrendered,
    Escaping,
    Leaving,
    MovingBackwards,
    Trapped
}
