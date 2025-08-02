using UnityEngine;
using UnityEngine.Events;

public class OnAwake : MonoBehaviour
{
    public UnityEvent onAwake;
    readonly bool onStart = false;

    void Awake()
    {
        if (!onStart){
            onAwake.Invoke();
        }
    }

    void Start()
    {
        if (onStart){
            onAwake.Invoke();
        }
    }
}