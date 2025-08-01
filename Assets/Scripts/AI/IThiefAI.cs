using UnityEngine;

public interface IThiefAI
{
    void Surrender();
    void Trap(float trapTime);
    Transform Transform{ get; }
}