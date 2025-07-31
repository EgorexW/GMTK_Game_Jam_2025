using Sirenix.OdinInspector;
using UnityEngine;

public class BoxNode : Node
{
    [BoxGroup("References")][Required][SerializeField] Box box;
    
    protected override bool IsInteractive => true;

    protected override void Awake()
    {
        base.Awake();
        box.onChange.AddListener(OnBoxStateChange);
    }

    void OnBoxStateChange()
    {
        active = box.On;
    }
}