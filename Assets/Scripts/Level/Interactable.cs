using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public bool isHoldable;

    void OnTriggerEnter2D(Collider2D other)
    {
        var interacter = General.GetComponentFromCollider<Interacter>(other);
        if (interacter != null){
            interacter.Select(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var interacter = General.GetComponentFromCollider<Interacter>(other);
        if (interacter != null){
            interacter.Deselect(this);
        }
    }

    public void Interact()
    {
        onInteract.Invoke();
    }
}