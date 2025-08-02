using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(LayoutElement))]
public class Tooltip : MonoBehaviour
{
    [SerializeField] [GetComponent] LayoutElement layoutElement;
    [SerializeField] [GetComponent] RectTransform rectTransform;
    [SerializeField] [Required] TextMeshProUGUI headerText;
    [SerializeField] [Required] TextMeshProUGUI descriptionText;

    void Awake()
    {
        Hide();
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        var desiredWidth = Mathf.Max(headerText.preferredWidth, descriptionText.preferredWidth);
        layoutElement.enabled = desiredWidth > layoutElement.preferredWidth;

        rectTransform.anchoredPosition =
            new Vector2(Mathf.Max(rectTransform.sizeDelta.x, rectTransform.anchoredPosition.x),
                Mathf.Max(rectTransform.sizeDelta.y, rectTransform.anchoredPosition.y));
    }

    void Show(string header, string description)
    {
        gameObject.SetActive(true);
        headerText.text = header;
        descriptionText.text = description;
        Update();
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