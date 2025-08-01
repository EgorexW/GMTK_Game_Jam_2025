using UnityEngine;

public class ImplaceableSpace : MonoBehaviour
{
    public void OnTriggerStay2D(Collider2D other)
    {
        var interacter = General.GetComponentFromCollider<Interacter>(other);
        if (interacter != null)
        {
            interacter.Release();
        }
    }
}
