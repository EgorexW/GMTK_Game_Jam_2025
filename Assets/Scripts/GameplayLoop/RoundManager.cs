using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class RoundManager : MonoBehaviour
{
    [SerializeField] float roundEndDelay = 3;
    [SerializeField] public List<Node> startNodes;
    
    [BoxGroup("References")][Required][SerializeField] public Transform guard;
    [FormerlySerializedAs("theifs")] [BoxGroup("References")][Required][SerializeField] public List<Transform> freeTheifs;

    [FoldoutGroup("Debug")][ShowInInspector] List<Transform> caughtTheifs = new List<Transform>();

    public Node GetStartNode()
    {
        return startNodes.Random();
    }
    
    public void TheifCaught(Transform theif, bool hasGem)
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

    public Transform GetCaughtTheif()
    {
        if (caughtTheifs.Count <= 0){
            return null;
        }
        var theif = caughtTheifs.Random();
        return theif;
    }

    public void TheifReleased(Transform transform)
    {
        freeTheifs.Add(transform);
        caughtTheifs.Remove(transform);
    }
}
