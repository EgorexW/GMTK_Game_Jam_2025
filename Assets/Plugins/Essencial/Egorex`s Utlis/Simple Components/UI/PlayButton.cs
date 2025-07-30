using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class PlayButton : MonoBehaviour, ISceneGetter
{
    [SerializeField][SceneObjectsOnly] protected string sceneName; 
    [SerializeField] Optional<TextMeshProUGUI> levelName = new Optional<TextMeshProUGUI>(null, false);
    [SerializeField] protected bool async = false;
    public void Play(){
        if (async){
            SceneManager.LoadSceneAsync(sceneName);
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }
    public void GetScene(string scene)
    {
        sceneName = scene;
        OnValidate();
    }
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
#if UNITY_EDITOR

    void Reset()
    {
        levelName = new(GetComponentInChildren<TextMeshProUGUI>());
        levelName.Enabled = levelName.Value != null;
        if (!TryGetComponent<Button>(out var button)){
            return;
        }
        for(var i = 0; i < button.onClick.GetPersistentEventCount(); i++){
            if (button.onClick.GetPersistentMethodName(i) == "Play"){
                return;
            }
        }
        UnityEventTools.AddPersistentListener(button.onClick, Play);
    }
#endif
}
