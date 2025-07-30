using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
}
