using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
#endif

[DefaultExecutionOrder(order: -100)][RequireComponent(typeof(TextMeshProUGUI))]
public class TextUI : CountUI
{
    const string STR_TO_SUBSTITUTE = "{nr}";

    [SerializeField] protected Optional<float> startValue = new(0, false);
    [SerializeField] float zeros = -1;
    [SerializeField] float valueModifier = 0;
    [InfoBox( STR_TO_SUBSTITUTE + " is substituted with the value")]
    [SerializeField][GetComponent] TextMeshProUGUI text;

    string format;

    protected virtual void Awake()
    {
        format = text.text;
        if (!format.Contains(STR_TO_SUBSTITUTE)){
            Debug.LogWarning("TextUI: Text does not contain " + STR_TO_SUBSTITUTE, this);
            format = STR_TO_SUBSTITUTE;
        }
        if (startValue){
            SetCount(startValue);
        }
    }
    public override void SetCount(float count)
    {
        if (format == null){
            Awake();
        }
        count += valueModifier;
        count = Mathf.Round(count * Mathf.Pow(10, -zeros)) / Mathf.Pow(10, -zeros);
        var textToShow = format.Replace(STR_TO_SUBSTITUTE, count.ToString());
        onUpdate.Invoke(count);
        UpdateUI(textToShow);
    }
    public void UpdateUI(string textToShow){
        text.text = textToShow;
    }

    public override void SetCount(int count)
    {
        SetCount((float)count);
    }

    void Reset()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
}
