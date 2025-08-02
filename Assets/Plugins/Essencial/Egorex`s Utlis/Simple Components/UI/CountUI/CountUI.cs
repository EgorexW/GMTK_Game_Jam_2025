using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
#endif

public abstract class CountUI : MonoBehaviour
{
    [FoldoutGroup("Events")] public UnityEvent<float> onUpdate;

    public virtual void SetCount(int count)
    {
        onUpdate.Invoke(count);
    }

    public virtual void SetCount(float count)
    {
        SetCount(Mathf.RoundToInt(count));
    }
}