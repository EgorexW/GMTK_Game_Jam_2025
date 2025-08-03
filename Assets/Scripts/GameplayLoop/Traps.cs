using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Traps : MonoBehaviour
{
    [SerializeField] List<GameObject> trapPrefabs;
    [SerializeField] List<Vector2> spawnPoints;

    [SerializeField] int startPoolTrapCount = 3;
    
    [FoldoutGroup("Debug")][ShowInInspector] List<GameObject> trapPool = new();
    [FoldoutGroup("Debug")] [ShowInInspector] List<GameObject> traps = new();

    void Awake()
    {
        AddTrapsToPool(startPoolTrapCount);
    }

    void AddTrapsToPool(int count = 1)
    {
        for (int i = 0; i < count; i++){
            foreach (var trap in trapPrefabs){
                trapPool.Add(trap);
            }
        }
    }

    public void ResetAll()
    {
        foreach (var trap in traps) trap.SetActive(true);
    }

    public void AddTraps()
    {
        foreach (var spawnPoint in spawnPoints){
            if (trapPool.Count == 0)
            {
                Debug.LogWarning("No traps available in the pool to spawn.");
                return;
            }
            var trap = trapPool.Random();
            trapPool.Remove(trap);
            Debug.Log($"Spawning trap {trap.name} at {spawnPoint}");
            traps.Add(Instantiate(trap, spawnPoint, Quaternion.identity, transform));
        }
        AddTrapsToPool();
    }
}