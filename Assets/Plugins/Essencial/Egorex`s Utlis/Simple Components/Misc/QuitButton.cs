using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class QuitButton : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.OSXEditor){
            Destroy(gameObject);
        }
    }
#if UNITY_EDITOR
    void Reset()
    {
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

    void Play()
    {
        Application.Quit();
    }
}