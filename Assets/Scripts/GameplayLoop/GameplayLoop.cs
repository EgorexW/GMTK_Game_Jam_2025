using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayLoop : MonoBehaviour
{
    public static GameplayLoop i { get; private set; }
    
    [FoldoutGroup("Debug")] [ShowInInspector] int loopNr = 1;

    
    void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(gameObject);
            return;
        }
        i = this;
        DontDestroyOnLoad(gameObject);
        StartNewRound(false);
    }

    public void EndRound(bool won)
    {
        if (won)
        {
            Debug.Log($"Round {loopNr} won!");
        }
        else
        {
            Debug.Log($"Round {loopNr} lost!");
        }
        loopNr++;
        StartNewRound();
    }

    void StartNewRound(bool reset = true)
    {
        Debug.Log($"Starting round {loopNr}...");
        if (reset)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}