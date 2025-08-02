using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class PlayButton : MonoBehaviour, ISceneGetter
{
    [SerializeField] [SceneObjectsOnly] protected string sceneName;
    [SerializeField] Optional<TextMeshProUGUI> levelName = new(null, false);
    [SerializeField] protected bool async;
#if UNITY_EDITOR

    void Reset()
    {
        levelName = new Optional<TextMeshProUGUI>(GetComponentInChildren<TextMeshProUGUI>());
        levelName.Enabled = levelName.Value != null;
        if (!TryGetComponent<Button>(out var button)){
            return;
        }
        for (var i = 0; i < button.onClick.GetPersistentEventCount(); i++)
            if (button.onClick.GetPersistentMethodName(i) == "Play"){
                return;
            }
        UnityEventTools.AddPersistentListener(button.onClick, Play);
    }
#endif
    void OnValidate()
    {
        if (!levelName){
            return;
        }
        if (levelName.Value == null){
            levelName = GetComponentInChildren<TextMeshProUGUI>();
        }
        if (levelName.Value == null){
            return;
        }
        levelName.Value.text = sceneName;
    }

    public void GetScene(string scene)
    {
        sceneName = scene;
        OnValidate();
    }

    public void Play()
    {
        if (async){
            SceneManager.LoadSceneAsync(sceneName);
        }
        else{
            SceneManager.LoadScene(sceneName);
        }
    }
}