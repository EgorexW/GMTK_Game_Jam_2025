using UnityEngine;

public class ImplaceableSpace : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var interacter = General.GetComponentFromCollider<Interacter>(other);
        if (interacter != null)
        {
            interacter.canHold = false;
            interacter.Release();
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        var interacter = General.GetComponentFromCollider<Interacter>(other);
        if (interacter != null)
        {
            interacter.canHold = true;
        }
    }
}
