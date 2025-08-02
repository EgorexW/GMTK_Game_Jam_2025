using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class TriggerLight : MonoBehaviour
{
    [SerializeField] [GetComponent] Light2D lights;

    [SerializeField] GameObject triggerObject;
    
    [SerializeField] float fadeSpeed = 1f;
    
    float onIntensity;
    float targetIntensity;

    void Awake()
    {
        onIntensity = lights.intensity;
        lights.intensity = 0f;
    }
    
    void Update()
    {
        lights.intensity = Mathf.MoveTowards(lights.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerObject == other.attachedRigidbody.gameObject){
            targetIntensity = onIntensity;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (triggerObject == other.attachedRigidbody.gameObject){
            targetIntensity = 0f;
        }
    }
}