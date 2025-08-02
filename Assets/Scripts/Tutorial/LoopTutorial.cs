using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class LoopTutorial : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] GameObject tutorialObject;
    
    [SerializeField] float timeActive = 10f;
    
    void Start()
    {
        if (PlayerPrefs.GetInt("Loop Tutorial", 0) == 1){
            return;
        }
        var loopNr = GameplayLoop.i.loopNr;
        if (loopNr != 2){
            return;
        }
        tutorialObject.SetActive(true);
        Destroy(gameObject, timeActive);
        PlayerPrefs.SetInt("Loop Tutorial", 1);
    }
}
