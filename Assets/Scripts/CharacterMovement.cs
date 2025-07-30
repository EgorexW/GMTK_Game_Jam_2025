using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Transform transformToRotate;
    
    [SerializeField] float moveSpeed = 5;
    
    Vector2 moveInput;
    

    void Update()
    {
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    public void MoveInput(Vector2 input)
    {
        moveInput = input;
    }
    public void RotateInput(Vector2 input)
    {
        input -= (Vector2)transform.position;
        if (input == Vector2.zero) return;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        transformToRotate.rotation = Quaternion.Euler(0, 0, angle);
    }
}
