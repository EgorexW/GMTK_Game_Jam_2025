using UnityEngine;
using UnityEngine.SceneManagement;

public class OnKeyDownRestart : OnKeyDown
{
    [SerializeField] protected bool async;
    public void Restart()
    {
        var sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (async){
            SceneManager.LoadSceneAsync(sceneBuildIndex);
        } else {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
    void Awake()
    {
        onKeyDown.AddListener(Restart);
    }
}