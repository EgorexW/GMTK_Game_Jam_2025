using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class RoundManager : MonoBehaviour
{
    [SerializeField] float roundEndDelay = 3;
    [SerializeField] public List<Node> startNodes;
    [SerializeField] float theifNr = 2;
    
    [BoxGroup("References")][Required][SerializeField] public Transform guard;
    
    [BoxGroup("References")][Required][SerializeField] public GameObject theifPrefab;
    
    
    [FoldoutGroup("Debug")][ShowInInspector] List<IThiefAI> freeTheifs = new List<IThiefAI>();
    [FoldoutGroup("Debug")][ShowInInspector] List<IThiefAI> caughtTheifs = new List<IThiefAI>();


    void Start()
    {
        for (int i = 0; i < theifNr; i++)
        {
            var theif = Instantiate(theifPrefab, Vector2.down * 100, Quaternion.identity, transform).GetComponent<IThiefAI>();
            theif.RoundManager = this;
            freeTheifs.Add(theif);
        }
    }

    public Node GetStartNode()
    {
        var freeStartNodes = startNodes.Copy();
        foreach (var freeTheif in freeTheifs){
            freeStartNodes.Remove(freeTheif.startNode);
        }
        if (freeStartNodes.Count > 0){
            return freeStartNodes.Random();
        }
        Debug.LogWarning("No free start nodes left, returning random node");
        return startNodes.Random();
    }
    
    public void TheifCaught(IThiefAI theif, bool hasGem)
    {
        if (hasGem)
        {
            EndRound(true);
        }
        freeTheifs.Remove(theif);
        caughtTheifs.Add(theif);
        if (freeTheifs.Count == 0){
            EndRound(true);
        }
    }

    void EndRound(bool won)
    {
        General.CallAfterSeconds(() => GameplayLoop.i.EndRound(won), roundEndDelay);
    }

    public void GameLost()
    {
        EndRound(false);
    }

    public IThiefAI GetCaughtTheif()
    {
        if (caughtTheifs.Count <= 0){
            return null;
        }
        var theif = caughtTheifs.Random();
        return theif;
    }

    public void TheifReleased(IThiefAI transform)
    {
        freeTheifs.Add(transform);
        caughtTheifs.Remove(transform);
    }
}
