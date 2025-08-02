using UnityEngine;

public class MoveNoise : MonoBehaviour
{
    public float range = 10f;

    public void OnMove()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hit in hits){
            var noiseHearer = General.GetComponentFromCollider<INoiseHearer>(hit);
            if (noiseHearer != null){
                noiseHearer.NoiseHeard(transform.position);
            }
        }
    }
}

public interface INoiseHearer
{
    void NoiseHeard(Vector2 position);
}