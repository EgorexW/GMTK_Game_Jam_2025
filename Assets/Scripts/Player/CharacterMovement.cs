using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Transform transformToRotate;
    // [BoxGroup("References")][Required][SerializeField] Rigidbody2D rigidbody;

    [SerializeField] float moveSpeed = 5;

    [FoldoutGroup("Events")] public UnityEvent onMove;

    Vector2 moveInput;

    void Update()
    {
        if (moveInput == Vector2.zero){
            return;
        }
        onMove?.Invoke();
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    public void MoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void RotateInput(Vector2 input)
    {
        input -= (Vector2)transform.position;
        if (input == Vector2.zero){
            return;
        }

        var angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        transformToRotate.rotation = Quaternion.Euler(0, 0, angle);
    }
}