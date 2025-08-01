using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

public class Traps : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] List<GameObject> trapPrefabs;
    
    [SerializeField] List<Vector2> spawnPoints;

    [FoldoutGroup("Debug")] [ShowInInspector] List<GameObject> traps = new List<GameObject>();

    public void ResetAll()
    {
        foreach (var trap in traps)
        {
            trap.SetActive(true);
        }
    }

    public void AddTrap()
    {
        var trap = trapPrefabs.Random();
        var spawnPoint = spawnPoints.Random();
        traps.Add(Instantiate(trap, spawnPoint, Quaternion.identity, transform));
    }
}
