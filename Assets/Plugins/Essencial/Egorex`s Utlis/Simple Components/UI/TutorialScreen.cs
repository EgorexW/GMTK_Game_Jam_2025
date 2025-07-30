using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] string prefabName = "Tutorial";
    [SerializeField] bool autoUpdatePrefabName = true;
    [SerializeField] bool activateOnAwake = true;

    void Awake()
    {
        UpdatePrefabName();
        gameObject.SetActive(false);
        if (activateOnAwake){
            TryToActivate();
        }
    }

    public void TryToActivate()
    {
        var activate = PlayerPrefs.GetInt(prefabName, 0) == 0;
        if (activate){
            Activate();
        }
    }

    public void Complete()
    {
        if (!gameObject.activeSelf){
            return;
        }
        PlayerPrefs.SetInt(prefabName, 1);
        Deactivate();
    }

    protected virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnValidate()
    {
        UpdatePrefabName();
    }

    void UpdatePrefabName()
    {
        if (autoUpdatePrefabName){
            prefabName = gameObject.name;
        }
    }
}
