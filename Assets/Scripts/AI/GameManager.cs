using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Node> startNodes;
    [BoxGroup("References")][Required][SerializeField] public Transform guard;

    public Node GetStartNode()
    {
        return startNodes.Random();
    }

    public void GameLost()
    {
        Debug.Log("Gem stolen, game lost!");
    }
}
