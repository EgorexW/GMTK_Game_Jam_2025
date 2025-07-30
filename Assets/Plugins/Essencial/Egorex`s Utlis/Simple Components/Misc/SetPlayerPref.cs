using UnityEngine;
using Sirenix.OdinInspector;

public class SetPlayerPref : MonoBehaviour
{
    [SerializeField] string prefName;
    [SerializeField] ObjectType prefType;
    [ShowIf("IsInt")]
    [SerializeField] int nrInt;
    [SerializeField] bool IsInt{
        get {
            return prefType == ObjectType.Int;
        }
    }
    [ShowIf("IsFloat")]
    [SerializeField] float nrFloat;
    [SerializeField] bool IsFloat{
        get {
            return prefType == ObjectType.Float;
        }
    }
    [ShowIf("IsString")]
    [SerializeField] string text;
    public bool IsString{
        get {
            return prefType == ObjectType.String;
        }
    }

    public void Set(){
        switch (prefType){
            case ObjectType.Int:
                SetInt();
                break;
            case ObjectType.Float:
                SetFloat();
                break;
            case ObjectType.String:
                SetString();
                break;
        }
    }
    void SetInt(){
        PlayerPrefs.SetInt(prefName, nrInt);
    }
    void SetFloat(){
        PlayerPrefs.SetFloat(prefName, nrFloat);
    }
    void SetString(){
        PlayerPrefs.SetFloat(prefName, nrFloat);
    }
}
