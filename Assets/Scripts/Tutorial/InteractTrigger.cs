using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Interactable interactable;
    
    bool flipInteractable = true;

    [FoldoutGroup("Events")] public UnityEvent onTrigger;

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
