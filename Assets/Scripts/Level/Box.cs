using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    [FoldoutGroup("Debug")][ShowInInspector] protected bool on = true;
    public bool On => on;

    [FoldoutGroup("Events")] public UnityEvent onChange;

    void Awake()
    {
        ActivateBox();
    }

    public virtual void ActivateBox()
    {
        on = true;
        onChange?.Invoke();
    }

    public virtual void DeactivateBox()
    {
        on = false;
        onChange?.Invoke();
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