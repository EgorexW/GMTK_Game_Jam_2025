using UnityEngine;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    [SerializeField] Animation[] animations;
    [SerializeField] bool startAnimationOnAwake = true;
    [SerializeField] UnityEvent onNewFrame;
    Animation activeAnimation;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (startAnimationOnAwake){
            activeAnimation = animations[0];
        }
    }
    public void SetAnimation(string animationName){
        if (activeAnimation != null && activeAnimation.name == animationName){
            return;
        }
        var animation = Array.Find<Animation>(animations, x => x.name == animationName);
        if (animation == null){
            Debug.LogWarning("Animation" + animationName + "does not exist");
            return;
        }
        activeAnimation = animation;
        activeAnimation.Restart();
    }
    public Animation GetAnimation(){
        return activeAnimation;
    }
    public Animation GetSetAnimation(string animationName){
        SetAnimation(animationName);
        return GetAnimation();
    }
    public void StopAnimation(){
        activeAnimation = null;
    }
    void Update(){
        if (activeAnimation == null){
            return;
        }
        var prevSprite = spriteRenderer.sprite;
        var newSprite = activeAnimation.GetNextFrame(Time.deltaTime);
        if (prevSprite != newSprite){
            onNewFrame.Invoke();
        }
        spriteRenderer.sprite = newSprite;
    }
}
