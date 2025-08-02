using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Message message;

    [SerializeField] float delay = 0.5f;

    LTDescr delayCall = new();

    void Reset()
    {
        message.header = gameObject.name;
    }

    void OnDisable()
    {
        Deactivate();
    }

    void OnMouseEnter()
    {
        Activate();
    }

    void OnMouseExit()
    {
        Deactivate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Activate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deactivate();
    }

    void Activate()
    {
        if (!enabled){
            return;
        }
        delayCall = LeanTween.delayedCall(delay, () => { TooltipSystem.Show(message); });
    }

    void Deactivate()
    {
        LeanTween.cancel(delayCall.uniqueId);
        TooltipSystem.Hide();
    }

    public void SetMessage(Message message, bool enable = true)
    {
        this.message = message;
        if (enable){
            Enable();
        }
    }

    public void Disable()
    {
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }
}