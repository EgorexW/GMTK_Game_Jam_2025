using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayLoop : MonoBehaviour
{
    public static GameplayLoop i { get; private set; }

    [SerializeField] string gameWinScene = "Game Won";

    [SerializeField] public string difficultyName = "Normal";

    [BoxGroup("References")] [Required] [SerializeField] Traps traps;
    [FoldoutGroup("Debug")] [ShowInInspector] public int loopNr{ private set; get; } = 1;
    
    void Awake()
    {
        if (i != null && i != this)
        {
            Delete();
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
            SceneManager.LoadSceneAsync(gameWinScene);
        }
        else
        {
            Debug.Log($"Round {loopNr} lost!");
            loopNr++;
            StartNewRound();
        }
    }

    void StartNewRound(bool reset = true)
    {
        Debug.Log($"Starting round {loopNr}...");
        traps.ResetAll();
        if (loopNr > 1){
            traps.AddTrap();
        }
        if (reset)
        {
            SceneManager.LoadSceneAsync("Gameplay");
        }
    }

    public void Delete()
    {
        Destroy(traps);
        Destroy(gameObject);
    }
}