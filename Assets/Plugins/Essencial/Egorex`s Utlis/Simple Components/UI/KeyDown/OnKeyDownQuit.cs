using UnityEngine;

public class OnKeyDownQuit : OnKeyDown
{
    void Awake()
    {
        onKeyDown.AddListener(CloseGame);
    }

    public static void CloseGame(){
        #if !UNITY_WEBGL && !UNITY_EDITOR
        Application.Quit();
        #else
        Debug.LogWarning("Application.Quit() is not supported in WebGL or Editor mode.");
        #endif
    }
}