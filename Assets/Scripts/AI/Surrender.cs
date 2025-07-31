using System;
using UnityEngine;

public class Surrender : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var thief = General.GetComponentFromCollider<ThiefAI>(other);
        if (thief == null){
            return;
        }
        var dir = (thief.transform.position - transform.position).normalized;
        var hit = Physics2D.Raycast(transform.position, dir);
        if (General.GetComponentFromCollider<ThiefAI>(hit.collider) != thief){
            return;
        }
        thief.Surrender();
    }
}
