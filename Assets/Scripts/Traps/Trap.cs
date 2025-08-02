using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    [SerializeField] float trapTime = 10;

    [FoldoutGroup("Events")] public UnityEvent onTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        var theif = General.GetComponentFromCollider<IThiefAI>(other);
        if (theif == null){
            return;
        }
        theif.Trap(trapTime);
        onTrigger.Invoke();
        General.CallAfterSeconds(() => gameObject.SetActive(false), trapTime);
    }
}