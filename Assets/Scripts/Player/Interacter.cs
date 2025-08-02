using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [SerializeField] Transform interactPrompt;
    [SerializeField] Vector2 holdPos = new(1, 0f);

    public bool canHold = true;

    Transform heldTransform;

    [FoldoutGroup("Debug")] [ShowInInspector] Interactable interactable;
    [FoldoutGroup("Debug")] [ShowInInspector] HashSet<Interactable> interactablesInRange = new();

    void Awake()
    {
        interactPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (heldTransform == null){
            return;
        }
        heldTransform.position = transform.position + (Vector3)holdPos;
    }

    public void Select(Interactable interactable)
    {
        interactablesInRange.Add(interactable);
        if (this.interactable != null){
            return;
        }
        interactPrompt.parent = interactable.transform;
        interactPrompt.localPosition = Vector3.zero;
        interactPrompt.gameObject.SetActive(true);
        this.interactable = interactable;
    }

    public void Deselect(Interactable interactable)
    {
        interactablesInRange.Remove(interactable);
        if (this.interactable == interactable){
            this.interactable = null;
        }
        interactPrompt.gameObject.SetActive(false);
        if (interactablesInRange.Count > 0){
            Select(interactablesInRange.First());
        }
    }

    public void Interact()
    {
        if (interactable != null){
            if (interactable.isHoldable){
                Hold(interactable.transform);
            }
            else{
                interactable.Interact();
            }
        }
    }

    public void Hold(Transform obj)
    {
        if (!canHold){
            Debug.LogWarning("Cannot hold object, Interacter is not allowed to hold objects!");
            return;
        }
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