using Sirenix.OdinInspector;
using Nrjwolf.Tools.AttachAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(LayoutElement))]
public class Tooltip : MonoBehaviour
{
    [SerializeField] [Required] TextMeshProUGUI headerText;
    [SerializeField] [Required] TextMeshProUGUI descriptionText;

    [SerializeField] [GetComponent] LayoutElement layoutElement;
    [SerializeField] [GetComponent] RectTransform rectTransform;
    
    void Awake()
    {
        Hide();
    }

    void Show(string header, string description)
    {
        gameObject.SetActive(true);
        headerText.text = header;
        descriptionText.text = description;
        Update();
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        var desiredWidth = Mathf.Max(headerText.preferredWidth, descriptionText.preferredWidth);
        layoutElement.enabled = desiredWidth > layoutElement.preferredWidth;

        rectTransform.anchoredPosition = new(Mathf.Max(rectTransform.sizeDelta.x, rectTransform.anchoredPosition.x), Mathf.Max(rectTransform.sizeDelta.y, rectTransform.anchoredPosition.y));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Message message)
    {
        Show(message.header, message.description);
    }
}
