using System;
using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class ThiefAI : MonoBehaviour
{
    [SerializeField][GetComponent] AIPath aiPath;
    
    [FormerlySerializedAs("nodes")] [BoxGroup("References")][Required][SerializeField] AI ai;
    
    [SerializeField][BoxGroup("Config")] List<Transform> seePoints;
    
    [SerializeField] float waitTime = 4;
    [SerializeField] float runAwayTime = 2;
    [SerializeField] float seeDis = 20;
    [SerializeField] float movementSeeMod = 0.5f;
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float runSpeed = 8;
    [SerializeField] float leaveTime = 5;
    
    [SerializeField] List<MoveToken> moveTokens = new List<MoveToken>();
    
    [FoldoutGroup("Debug")][ShowInInspector] bool hasGem = false;
    [FoldoutGroup("Debug")][ShowInInspector] Node startNode;
    [FoldoutGroup("Debug")][ShowInInspector] Node node;
    [FoldoutGroup("Debug")][ShowInInspector] float currentWaitTime = 0;
    [FoldoutGroup("Debug")][ShowInInspector] State state;
    [FoldoutGroup("Debug")][ShowInInspector] float guardSeenTime;

    void Start()
    {
        Begin();
    }

    void Begin()
    {
        startNode = ai.GetStartNode();
        node = startNode;
        var position = startNode.transform.position;
        aiPath.Teleport(position);
        aiPath.destination = position;
        aiPath.maxSpeed = moveSpeed;
        state = State.Moving;
        ReachedDestination();
    }

    void Update()
    {
        switch (state){
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
            case State.Waiting when LookForGuard():
                return;
            case State.Waiting:
                Wait();
                return;
            case State.Moving when LookForGuard():
                return;
            case State.Moving:{
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
            ai.GameLost();
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
            Vector2 targetDir = ai.guard.transform.position - point.position;
            var hit = Physics2D.Raycast(point.position, targetDir, seeDis);
            var componentFromCollider = General.GetComponentFromCollider<Transform>(hit.collider);
            // Debug.Log(componentFromCollider);
            if (componentFromCollider != ai.guard.transform){
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
        node = startNode;
        state = State.Escaping;
        aiPath.maxSpeed = runSpeed;
        SetDestination(node.transform.position);
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
        startNode = ai.GetStartNode();
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
        var moveToken = moveTokens.Random();
        if (!node.HasSideNodes){
            moveToken = MoveToken.Forward;
        }
        moveTokens.Remove(moveToken);
        switch (moveToken){
            case MoveToken.Forward:
                moveTokens.Add(MoveToken.Forward);
                moveTokens.Add(MoveToken.Side);
                node = node.GetForwardNode();
                break;
            case MoveToken.Side:
                moveTokens.Add(MoveToken.Forward);
                node = node.GetSideNode();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        state = State.Moving;
        SetDestination(node.transform.position);
    }

    void SetDestination(Vector2 destination)
    {
        aiPath.destination = destination;
    }

    public void Surrender()
    {
        aiPath.canMove = false;
        state = State.Surrendered;
        Debug.Log("Surrendered!");
    }
}

enum MoveToken
{
    Forward,
    Side,
}

enum State
{
    Moving,
    Waiting,
    Surrendered,
    Escaping,
    Leaving
}
