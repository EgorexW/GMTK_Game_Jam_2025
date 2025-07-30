using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputHandler : MonoBehaviour
{
    [BoxGroup("References")][SerializeField][GetComponent] PlayerInput playerInput;
    
    [FormerlySerializedAs("playerMovement")] [BoxGroup("References")][Required][SerializeField] CharacterMovement characterMovement;

    void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMovePerformed;
        playerInput.actions["Move"].canceled += OnMovePerformed;
        playerInput.actions["Look"].performed += OnRotatePerformed;
    }

    void OnRotatePerformed(InputAction.CallbackContext obj)
    {
        var mouseScreenPos = obj.ReadValue<Vector2>();
        var mouseWorldPos = General.GetMouseWorldPos(mouseScreenPos);
        characterMovement.RotateInput(mouseWorldPos);
    }

    void OnMovePerformed(InputAction.CallbackContext obj)
    {
        characterMovement.MoveInput(obj.ReadValue<Vector2>());
    }
}