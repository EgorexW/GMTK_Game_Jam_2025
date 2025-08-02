using System;
using UnityEngine;

public class SpriteObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (TryGetComponent(out SpriteRenderer foundSpriteRenderer)){
            spriteRenderer = foundSpriteRenderer;
            return;
        }
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }

    public void ApplySettings(VisualSettings visualSettings)
    {
        spriteRenderer.sprite = visualSettings.sprite;
        spriteRenderer.color = visualSettings.color;
    }
}

[Serializable]
public class VisualSettings
{
    public Sprite sprite;
    public Color color;

    public VisualSettings(Sprite sprite, Color color)
    {
        this.sprite = sprite;
        this.color = color;
    }
}