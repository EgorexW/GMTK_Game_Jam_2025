using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    [FoldoutGroup("Events")] public UnityEvent<ThiefAI> onTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var theif = General.GetComponentFromCollider<ThiefAI>(other);
        if (theif != null){
            onTrigger.Invoke(theif);
        }
    }
}
