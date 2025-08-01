using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] float trapTime = 10;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var theif = General.GetComponentFromCollider<IThiefAI>(other);
        if (theif != null){
            theif.Trap(trapTime);
            gameObject.SetActive(false);
        }
    }
}
