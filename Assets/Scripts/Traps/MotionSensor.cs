using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    [FoldoutGroup("Events")] public UnityEvent<IThiefAI> onTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        var theif = General.GetComponentFromCollider<IThiefAI>(other);
        if (theif != null){
            onTrigger.Invoke(theif);
        }
    }
}