using System;
using Nrjwolf.Tools.AttachAttributes;
using Pathfinding;
using UnityEngine;

public class ThiefAI : MonoBehaviour
{
    [SerializeField][GetComponent] AIPath aiPath;
    

    void SetDestination(Vector2 destination)
    {
        aiPath.destination = destination;
    }
}
