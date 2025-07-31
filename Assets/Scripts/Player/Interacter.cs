using Sirenix.OdinInspector;
using UnityEngine;


    public class Interacter : MonoBehaviour
    {
        [FoldoutGroup("Debug")][ShowInInspector] Interactable interactable;

        public void Select(Interactable interactable)
        {
            this.interactable = interactable;
        }
        public void Deselect(Interactable interactable)
        {
            if (this.interactable == interactable)
            {
                this.interactable = null;
            }
        }
        
        public void Interact()
        {
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
