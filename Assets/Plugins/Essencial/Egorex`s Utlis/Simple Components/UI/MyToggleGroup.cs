using UnityEngine;
using UnityEngine.UI;

public class MyToggleGroup : ToggleGroup
{
    [SerializeField] bool addChildrenOnAwake;
    
    protected override void Awake()
    {
        base.Awake();
        if (addChildrenOnAwake){
            AddChildren();
        }
    }

    void AddChildren()
    {
        foreach (var toggle in GetComponentsInChildren<Toggle>()){
            toggle.group = this;
        }
    }

    public void ApplyToggleGroup(GameObject obj)
    {
        obj.GetComponent<Toggle>().group = this;
    }
}
