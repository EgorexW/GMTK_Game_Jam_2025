using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] List<GameObject> trapPrefabs;

    [SerializeField] List<Vector2> spawnPoints;

    [FoldoutGroup("Debug")] [ShowInInspector] List<GameObject> traps = new();

    public void ResetAll()
    {
        foreach (var trap in traps) trap.SetActive(true);
    }

    public void AddTraps()
    {
        foreach (var spawnPoint in spawnPoints){
            var trap = trapPrefabs.Random();
            traps.Add(Instantiate(trap, spawnPoint, Quaternion.identity, transform));
        }
    }
}