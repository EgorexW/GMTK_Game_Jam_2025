using System;
using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ThiefAI : MonoBehaviour, IThiefAI
{
    [SerializeField][GetComponent] AIPath aiPath;
    
    [BoxGroup("References")][Required][SerializeField] new Collider2D collider2D;
    [BoxGroup("References")][Required][SerializeField] Node releaseNode;
    
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
    [SerializeField] float tryRescueChance = 0.2f;
    
    [FoldoutGroup("Debug")][ShowInInspector] bool hasGem = false;
    [FoldoutGroup("Debug")][ShowInInspector] public Node startNode { get; private set; }
    [FoldoutGroup("Debug")][ShowInInspector] Node node;
    [FoldoutGroup("Debug")][ShowInInspector] float currentWaitTime = 0;
    [FoldoutGroup("Debug")][ShowInInspector] State state;
    [FoldoutGroup("Debug")][ShowInInspector] float guardSeenTime;
    [FoldoutGroup("Debug")][ShowInInspector] int sideMovementsLeft;
    [FoldoutGroup("Debug")][ShowInInspector] float trapTime;
    
    public Transform Transform => transform;
    public RoundManager RoundManager { get; set; }

    void Awake()
    {
        releaseNode.enabled = false;
    }

    void Start()
    {
        Begin();
    }

    void Begin()
    {
        startNode = RoundManager.GetStartNode();
        node = startNode;
        var position = startNode.transform.position;
        aiPath.Teleport(position);
        aiPath.destination = position;
        aiPath.maxSpeed = moveSpeed;
        sideMovementsLeft = sideMovementsPerTry;
        state = State.Moving;
        if (Random.value < tryRescueChance){
            var caughtTheif = RoundManager.GetCaughtTheif();
            if (caughtTheif != null){
                node = caughtTheif.Transform.GetComponent<Node>();
                SetDestination(node.transform.position);
                return;
            }
        }
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
                    RunAway(false);
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
            case State.Waiting or State.Moving /*or State.MovingBackwards*/ when LookForGuard():
                return;
            case State.Waiting:
                Wait();
                return;
            case State.Moving /*or State.MovingBackwards*/:{
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
            Vector2 targetDir = RoundManager.guard.transform.position - point.position;
            var hit = Physics2D.Raycast(point.position, targetDir, seeDis);
            var componentFromCollider = General.GetComponentFromCollider<Transform>(hit.collider);
            // Debug.Log(componentFromCollider);
            if (componentFromCollider != RoundManager.guard.transform){
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
        guardSeenTime += Time.deltaTime * seeMod;
        if (guardSeenTime >= runAwayTime){
            RunAway(true);
        }
    }

    void RunAway(bool seesGuard)
    {
        node = SelectEscapeNode(seesGuard);
        startNode = null;
        state = State.Escaping;
        aiPath.maxSpeed = runSpeed;
        SetDestination(node.transform.position);
    }

    Node SelectEscapeNode(bool seesGuard)
    {
        if (seesGuard){
            var guardDir = (RoundManager.guard.transform.position - transform.position).normalized;
            var smallestDot = float.MaxValue;
            Node escapeNode = null;
            foreach (var node in RoundManager.startNodes){
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
        var lowestDistance = float.MaxValue;
        Node escapeNode2 = null;
        foreach (var nodeTmp in RoundManager.startNodes){
            var distance = Vector2.Distance(nodeTmp.transform.position, transform.position);
            if (distance > lowestDistance){
                continue;
            }
            lowestDistance = distance;
            escapeNode2 = nodeTmp;
        }
        return escapeNode2;
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
        RunAway(true);
    }

    void Leave()
    {
        if (hasGem){
            RoundManager.GameLost();
        }
        state = State.Leaving;
        aiPath.canMove = false;
        currentWaitTime = 0;
    }

    void SelectNewDestination()
    {
        node = SelectNextNode();
        if (node == null){
            Debug.LogWarning("No next node found! Running away!", this);
            RunAway(false);
            return;
        }
        state = State.Moving;
        SetDestination(node.transform.position);
    }

    Node SelectNextNode()
    {
        Node nodeTmp = null;
        if (Random.value < interactiveMovementChance){
            nodeTmp = node.GetRandomInteractiveNode();
            if (nodeTmp != null){
                return nodeTmp;
            }
        }
        if (sideMovementsLeft > 0 && Random.value < sideMovementChance){
            nodeTmp = node.GetRandomSideNode();
            if (nodeTmp != null){
                sideMovementsLeft--;
                return nodeTmp;
            }
        }
        nodeTmp = node.GetRandomForwardNode();
        return nodeTmp;
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
        RoundManager.TheifCaught(this, hasGem);
        collider2D.enabled = false;
        releaseNode.enabled = true;
    }

    public void Release()
    {
        aiPath.canMove = true;
        Debug.Log("Released!", this);
        collider2D.enabled = true;
        releaseNode.enabled = false;
        RoundManager.TheifReleased(this);
        RunAway(false);
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
    Trapped
}
