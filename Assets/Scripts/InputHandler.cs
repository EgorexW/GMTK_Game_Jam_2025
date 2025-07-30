using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [BoxGroup("References")][SerializeField][GetComponent] PlayerInput playerInput;
    
    [BoxGroup("References")][Required][SerializeField] PlayerMovement playerMovement;

    void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Move"].canceled += OnMovePerformed;
    }

    void OnMovePerformed(InputAction.CallbackContext obj)
    {
        playerMovement.MoveInput(obj.ReadValue<Vector2>());
    }
}