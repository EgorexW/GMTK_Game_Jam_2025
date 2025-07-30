using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ObjectButton<T> : MonoBehaviour
{
    T obj;
    UnityAction<T> callback;

    [FoldoutGroup("Events")] public UnityEvent<T> onClick;
    
    public virtual void SetObject(T obj)
    {
        this.obj = obj;
    }

    public virtual void SetCallback(UnityAction<T> callback)
    {
        this.callback = callback;
    }

    public void OnClick()
    {
        callback?.Invoke(obj);
        onClick.Invoke(obj);
    }
#if UNITY_EDITOR
    void Reset()
    {
        if (!TryGetComponent<Button>(out var button)){
            return;
        }
        for(var i = 0; i < button.onClick.GetPersistentEventCount(); i++){
            if (button.onClick.GetPersistentMethodName(i) == "Play"){
                return;
            }
        }
        UnityEventTools.AddPersistentListener(button.onClick, OnClick);
    }
#endif
}