using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIElement : MonoBehaviour
{
    public bool hideOnStart = true;

    [FoldoutGroup("Events")] public UnityEvent onShow;

    [FoldoutGroup("Events")] public UnityEvent onHide;

    protected void Start()
    {
        if (hideOnStart){
            Hide();
        }
        else{
            Show();
        }
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        onShow.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        onHide.Invoke();
    }
}