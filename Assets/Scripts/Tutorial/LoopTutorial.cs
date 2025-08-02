using Sirenix.OdinInspector;
using UnityEngine;

public class LoopTutorial : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject tutorialObject;

    [SerializeField] float timeActive = 10f;

    void Start()
    {
        var loopNr = GameplayLoop.i.loopNr;
        if (loopNr != 2){
            return;
        }
        tutorialObject.SetActive(true);
        Destroy(gameObject, timeActive);
    }
}