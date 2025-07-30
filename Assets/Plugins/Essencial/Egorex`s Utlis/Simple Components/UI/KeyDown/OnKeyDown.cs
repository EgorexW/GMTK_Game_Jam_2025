using UnityEngine;
using UnityEngine.Events;

public class OnKeyDown : MonoBehaviour
{
    public KeyCode actionKey = KeyCode.Escape;
    public UnityEvent onKeyDown = new();

    void Update()
    { 
        if (Input.GetKeyDown(actionKey))
        {
            onKeyDown.Invoke();
        }
    }
}
