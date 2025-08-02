using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Box : MonoBehaviour
{
    [GetComponent] [SerializeField] SpriteRenderer spriteRenderer;
    
    [BoxGroup("Sprites")] [SerializeField] Sprite onSprite;
    [BoxGroup("Sprites")] [SerializeField] Sprite offSprite;
    
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
        spriteRenderer.sprite = onSprite;
        onChange?.Invoke();
    }

    public virtual void DeactivateBox()
    {
        on = false;
        spriteRenderer.sprite = offSprite;
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