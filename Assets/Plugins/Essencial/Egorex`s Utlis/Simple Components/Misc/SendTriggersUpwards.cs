using UnityEngine;

public class SendTriggersUpwards : MonoBehaviour
{
    float timeSendMessage;
    
    void OnTriggerEnter(Collider other)
    {
        if (Mathf.Approximately(Time.time, timeSendMessage)){
            return;
        }
        timeSendMessage = Time.time;
        SendMessageUpwards("OnTriggerEnter", other, SendMessageOptions.DontRequireReceiver);
    }
}
