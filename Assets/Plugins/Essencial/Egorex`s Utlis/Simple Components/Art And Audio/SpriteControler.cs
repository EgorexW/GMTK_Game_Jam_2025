using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteControler : AnimationController
{
    bool facingRight = true;

    public void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void FaceLeft()
    {
        if (facingRight){
            Flip();
        }
    }

    public void FaceRight()
    {
        if (!facingRight){
            Flip();
        }
    }
}