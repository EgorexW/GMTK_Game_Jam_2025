using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class BoxNode : MonoBehaviour
{
    [GetComponent][SerializeField] Box box;
    [GetComponent][SerializeField] Node node;

    protected void Awake()
    {
        box.onChange.AddListener(OnBoxStateChange);
    }

    void OnBoxStateChange()
    {
        node.active = box.On;
    }
}