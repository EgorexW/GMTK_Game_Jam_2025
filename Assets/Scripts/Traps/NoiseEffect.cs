using Sirenix.OdinInspector;
using UnityEngine;

public class NoiseEffect : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Transform effectTransform;
    [BoxGroup("References")] [Required] [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] float range = 15f;
    [SerializeField] float time = 1;
    [SerializeField] float startAlpha = 0.5f;

    void Awake()
    {
        effectTransform.localScale = Vector3.zero;
    }

    public void ShowNoise()
    {
        effectTransform.localScale = Vector3.zero;
        effectTransform.LeanScale(Vector3.one * range, time);
        spriteRenderer.color = new Color(1f, 1f, 1f, startAlpha);
        LeanTween.color(spriteRenderer.gameObject, new Color(1f, 1f, 1f, 0f), time);
    }
}