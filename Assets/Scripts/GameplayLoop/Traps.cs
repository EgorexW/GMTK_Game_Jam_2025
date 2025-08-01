using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

public class Traps : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] GameObject trap;
    
    [SerializeField] Vector2 spawnPoint;

    [FoldoutGroup("Debug")] [ShowInInspector] List<GameObject> traps = new List<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ResetAll()
    {
        foreach (var trap in traps)
        {
            trap.SetActive(true);
        }
    }

    public void AddTrap()
    {
        traps.Add(Instantiate(trap, spawnPoint, Quaternion.identity, transform));
        // Debug.Log("Trap added at " + spawnPoint);
    }
}
