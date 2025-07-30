using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    [SerializeField] Message message;
    
    [SerializeField] float delay = 0.5f;
    
    LTDescr delayCall = new();

    public void OnPointerEnter(PointerEventData eventData)
    {
        Activate();
    }

    void OnMouseEnter()
    {
        Activate();
    }

    void Activate()
    {
        if (!enabled){
            return;
        }
        delayCall = LeanTween.delayedCall(delay, () =>
        {
            TooltipSystem.Show(message);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deactivate();
    }

    void OnMouseExit()
    {
        Deactivate();
    }

    void Deactivate()
    {
        LeanTween.cancel(delayCall.uniqueId);
        TooltipSystem.Hide();
    }

    void OnDisable()
    {
        Deactivate();
    }

    void Reset()
    {
        message.header = gameObject.name;
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
