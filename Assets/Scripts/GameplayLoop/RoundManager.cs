using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class RoundManager : MonoBehaviour
{
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
            GameplayLoop.i.EndRound(true);
        }
        freeTheifs.Remove(theif);
        caughtTheifs.Add(theif);
        if (freeTheifs.Count == 0)
        {
            GameplayLoop.i.EndRound(true);
        }
    }

    public void GameLost()
    {
        GameplayLoop.i.EndRound(false);
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
