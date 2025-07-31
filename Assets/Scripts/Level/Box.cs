using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    [FoldoutGroup("Debug")][ShowInInspector] protected bool on = true;
    public bool On => on;

    [FoldoutGroup("Events")] public UnityEvent onChange;

    public virtual void ActivateBox()
    {
        on = true;
    }

    public virtual void DeactivateBox()
    {
        on = false;
    }

    public void ChangeState()
    {
        if (on)
        {
            DeactivateBox();
        }
        else
        {
            ActivateBox();
        }
    }
}