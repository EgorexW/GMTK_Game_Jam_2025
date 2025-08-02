using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayLoop : MonoBehaviour
{
    [SerializeField] string gameWinScene = "Game Won";

    [SerializeField] public string difficultyName = "Normal";

    [BoxGroup("References")] [Required] [SerializeField] Traps traps;
    [FoldoutGroup("Debug")] [ShowInInspector] static bool roundRunning;
    public static GameplayLoop i{ get; private set; }
    [FoldoutGroup("Debug")] [ShowInInspector] public int loopNr{ private set; get; } = 1;

    void Awake()
    {
        roundRunning = true;
        if (i != null && i != this){
            Delete();
            return;
        }
        i = this;
        DontDestroyOnLoad(gameObject);
        StartNewRound(false);
    }

    public void EndRound(bool won)
    {
        if (!roundRunning){
            Debug.LogWarning("Tried to end a round that is not running!");
            return;
        }
        roundRunning = false;
        if (won){
            Debug.Log($"Round {loopNr} won!");
            SceneManager.LoadSceneAsync(gameWinScene);
        }
        else{
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
            traps.AddTraps();
        }
        if (reset){
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}