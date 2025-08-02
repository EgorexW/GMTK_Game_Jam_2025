using Sirenix.OdinInspector;
using UnityEngine;

public class NoiseEffect : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Transform effectTransform;
    [BoxGroup("References")] [Required] [SerializeField] SpriteRenderer spriteRenderer;
    
    [SerializeField] float range = 15f;
    [SerializeField] float time = 1;
    [SerializeField] float startAlpha = 0.5f;

    Color startColor;

    void Awake()
    {
        effectTransform.localScale = Vector3.zero;
        startColor = spriteRenderer.color;
        startColor.a = startAlpha;
    }

    public void ShowNoise()
    {
        effectTransform.localScale = Vector3.zero;
        effectTransform.LeanScale(Vector3.one * range, time);
        spriteRenderer.color = startColor;
        var endColor = startColor;
        endColor.a = 0;
        LeanTween.color(spriteRenderer.gameObject, endColor, time);
    }
}