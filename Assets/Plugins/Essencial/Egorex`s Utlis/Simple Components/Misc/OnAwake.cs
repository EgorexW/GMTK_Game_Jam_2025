using UnityEngine;
using UnityEngine.Events;

public class OnAwake : MonoBehaviour
{
    bool onStart = false;
    public UnityEvent onAwake;

    void Awake(){
        if (!onStart){
            onAwake.Invoke();
        }
    }
    void Start(){
        if (onStart){
            onAwake.Invoke();
        }
    }
}
