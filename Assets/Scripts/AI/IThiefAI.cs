using UnityEngine;

public interface IThiefAI
{
    Transform Transform{ get; }
    Node startNode{ get; }
    RoundManager RoundManager{ get; set; }
    void Surrender();
    void Trap(float trapTime);
}