using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class OnKeyDown : MonoBehaviour
{
    public bool dontDestroyOnLoad = false;
    public KeyCode actionKey = KeyCode.Escape;
    public UnityEvent onKeyDown = new();

    protected virtual void Awake()
    {
        if (dontDestroyOnLoad)
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
