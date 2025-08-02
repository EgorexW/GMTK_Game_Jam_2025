using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Interactable interactable;

    [FoldoutGroup("Events")] public UnityEvent onTrigger;

    readonly bool flipInteractable = true;

    void Start()
    {
        if (flipInteractable){
            interactable.Interact();
        }
        interactable.onInteract.AddListener(Trigger);
    }

    void Trigger()
    {
        onTrigger.Invoke();
    }
}