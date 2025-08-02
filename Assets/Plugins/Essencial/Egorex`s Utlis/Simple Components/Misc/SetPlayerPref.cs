using Sirenix.OdinInspector;
using UnityEngine;

public class SetPlayerPref : MonoBehaviour
{
    [SerializeField] string prefName;
    [SerializeField] ObjectType prefType;

    [ShowIf("IsInt")] [SerializeField] int nrInt;

    [ShowIf("IsFloat")] [SerializeField] float nrFloat;

    [ShowIf("IsString")] [SerializeField] string text;

    bool IsInt => prefType == ObjectType.Int;
    bool IsFloat => prefType == ObjectType.Float;
    public bool IsString => prefType == ObjectType.String;

    public void Set()
    {
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

    void SetInt()
    {
        PlayerPrefs.SetInt(prefName, nrInt);
    }

    void SetFloat()
    {
        PlayerPrefs.SetFloat(prefName, nrFloat);
    }

    void SetString()
    {
        PlayerPrefs.SetFloat(prefName, nrFloat);
    }
}