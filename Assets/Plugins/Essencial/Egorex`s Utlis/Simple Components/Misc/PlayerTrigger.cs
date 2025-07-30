using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] bool oneTime = true;
    [SerializeField] UnityEvent onPlayerTrigger;

    void Awake(){
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other){
        if (!other.CompareTag("Player")){
            return;
        }
        onPlayerTrigger.Invoke();
        if (oneTime){
            Destroy(gameObject);
        }
    }
}
