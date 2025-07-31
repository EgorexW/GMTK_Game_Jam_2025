using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] public List<Node> startNodes;
    [BoxGroup("References")][Required][SerializeField] public Transform guard;
    
    [BoxGroup("References")][Required][SerializeField] public List<Transform> theifs;

    public Node GetStartNode()
    {
        return startNodes.Random();
    }
    
    public void TheifCaught(Transform theif)
    {
        theifs.Remove(theif);
        if (theifs.Count == 0)
        {
            GameplayLoop.i.EndRound(true);
        }
    }

    public void GameLost()
    {
        GameplayLoop.i.EndRound(false);
    }
}
