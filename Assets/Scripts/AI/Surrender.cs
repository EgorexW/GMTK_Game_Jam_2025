using UnityEngine;

public class Surrender : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var thief = General.GetComponentFromCollider<IThiefAI>(other);
        if (thief == null){
            return;
        }
        var dir = (thief.Transform.position - transform.position).normalized;
        var hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, LayerMask.GetMask("Default"));
        if (General.GetComponentFromCollider<IThiefAI>(hit.collider) != thief){
            return;
        }
        thief.Surrender();
    }
}