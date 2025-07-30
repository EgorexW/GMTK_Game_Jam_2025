using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public enum CompareType{
    Bigger,
    Smaller,
    Equal
}
enum ObjectType{
    Int,
    Float,
    String, 
    Bool
}

public class CheckValue : MonoBehaviour
{
    [SerializeField] ObjectType objType;
    [ShowIf("IsInt")]
    [SerializeField] int nrInt;
    bool IsInt{
        get {
            return objType == ObjectType.Int;
        }
    }
    [ShowIf("IsFloat")]
    [SerializeField] float nrFloat;
    bool IsFloat{
        get {
            return objType == ObjectType.Float;
        }
    }
    [ShowIf("InNumber")]
    [SerializeField] CompareType compareType = CompareType.Equal;
    bool InNumber{
        get {
            return IsFloat || IsInt;
        }
    }
    [ShowIf("IsString")]
    [SerializeField] string text;
    bool IsString{
        get {
            return objType == ObjectType.String;
        }
    }
    [ShowIf("IsBool")]
    [SerializeField] bool desiredCondition;
    bool IsBool{
        get {
            return objType == ObjectType.Bool;
        }
    }
    [FoldoutGroup("Events")]
    public UnityEvent onTrue;
    [FoldoutGroup("Events")]
    public UnityEvent onFalse;
    [FoldoutGroup("Events")]
    public UnityEvent<bool> onCheck;

    public void GetCheck(object obj){
        switch (objType){
            case ObjectType.Int:
                CheckInt((int)obj);
                break;
            case ObjectType.Float:
                CheckFloat((float)obj);
                break;
            case ObjectType.String:
                CheckString((string)obj);
                break;
            case ObjectType.Bool:
                CheckBool((bool)obj);
                break;
        }
    }

    void ResolveResult(bool result)
    {
        if (result){
            onTrue.Invoke();
        } else {
            onFalse.Invoke();
        }
        onCheck.Invoke(result);
    }

    public void CheckBool(bool condition)
    {
        var result = condition == desiredCondition;
        ResolveResult(result);
    }

    public void CheckInt(int value){
        bool result;
        switch (compareType){
            case CompareType.Bigger:
                result = value > nrInt;
                break;
            case CompareType.Smaller:
                result = value < nrInt;
                break;
            case CompareType.Equal:
                result = value == nrInt;
                break;
            default:
                throw new System.NotSupportedException();
        }
        ResolveResult(result);
    }
    public void CheckFloat(float value){
        bool result;
        switch (compareType){
            case CompareType.Bigger:
                result = value > nrFloat;
                break;
            case CompareType.Smaller:
                result = value < nrFloat;
                break;
            case CompareType.Equal:
                result = Mathf.Approximately(value, nrFloat);
                break;
            default:
                throw new System.NotSupportedException();
        }
        ResolveResult(result);
    }
    public void CheckString(string value){
        var result = value == text;
        ResolveResult(result);
    }
}
