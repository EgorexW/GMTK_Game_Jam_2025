using System;
using Sirenix.OdinInspector;
using UnityEngine;


    public class Interacter : MonoBehaviour
    {
        [FoldoutGroup("Debug")][ShowInInspector] Interactable interactable;
        
        [SerializeField] Vector2 holdPos = new Vector2(1, 0f);
        
        Transform heldTransform;
        
        public void Select(Interactable interactable)
        {
            if (this.interactable != null){
                return;
            }
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
                if (interactable.isHoldable){
                    Hold(interactable.transform);
                }
                else{
                    interactable.Interact(this);
                }
            }
        }

        void Update()
        {
            if (heldTransform == null) return;
            heldTransform.position = transform.position + (Vector3)holdPos;
        }

        public void Hold(Transform obj)
        {
            if (heldTransform != null){
                Release();
                return;
            }
            heldTransform = obj;
        }

        public void Release()
        {
            heldTransform = null;
        }
    }
