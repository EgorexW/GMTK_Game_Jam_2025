using UnityEngine;
using UnityEngine.Events;

public class OnKeyDown : MonoBehaviour
{
    public bool DontDestroyOnLoad = false;
    public KeyCode actionKey = KeyCode.Escape;
    public UnityEvent onKeyDown = new();

    protected virtual void Awake()
    {
        if (DontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    { 
        if (Input.GetKeyDown(actionKey))
        {
            onKeyDown.Invoke();
        }
    }
}
