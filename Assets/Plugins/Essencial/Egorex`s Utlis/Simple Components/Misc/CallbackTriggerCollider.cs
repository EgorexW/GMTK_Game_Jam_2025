using UnityEngine;
using UnityEngine.Events;

public class CallbackTriggerCollider : MonoBehaviour
{
    public UnityEvent<Collider2D> callback;

    void Awake()
    {
        if (!gameObject.GetComponent<Collider2D>()){
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        }
        if (!gameObject.GetComponent<Rigidbody2D>()){
            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        callback.Invoke(other);
    }
}