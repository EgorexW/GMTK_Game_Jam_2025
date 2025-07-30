using Sirenix.OdinInspector;
using UnityEngine;

public class ValueUI : TextUI
{
    [SerializeField][Required] Value value;

    protected override void Awake()
    {
        base.Awake();
        value.OnValueChanged.AddListener(OnValueChanged);
        SetCount(value.GetValue());
    }

    void OnValueChanged(ValueChangeCallback valueChangeCallback)
    {
        SetCount(valueChangeCallback.newValue);
    }
}
