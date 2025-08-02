using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public enum CompareType
{
    Bigger,
    Smaller,
    Equal
}

enum ObjectType
{
    Int,
    Float,
    String,
    Bool
}

public class CheckValue : MonoBehaviour
{
    [SerializeField] ObjectType objType;

    [ShowIf("IsInt")] [SerializeField] int nrInt;

    [ShowIf("IsFloat")] [SerializeField] float nrFloat;

    [ShowIf("InNumber")] [SerializeField] CompareType compareType = CompareType.Equal;

    [ShowIf("IsString")] [SerializeField] string text;

    [ShowIf("IsBool")] [SerializeField] bool desiredCondition;

    [FoldoutGroup("Events")] public UnityEvent onTrue;

    [FoldoutGroup("Events")] public UnityEvent onFalse;

    [FoldoutGroup("Events")] public UnityEvent<bool> onCheck;

    bool IsInt => objType == ObjectType.Int;
    bool IsFloat => objType == ObjectType.Float;
    bool InNumber => IsFloat || IsInt;
    bool IsString => objType == ObjectType.String;
    bool IsBool => objType == ObjectType.Bool;

    public void GetCheck(object obj)
    {
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
        }
        else{
            onFalse.Invoke();
        }
        onCheck.Invoke(result);
    }

    public void CheckBool(bool condition)
    {
        var result = condition == desiredCondition;
        ResolveResult(result);
    }

    public void CheckInt(int value)
    {
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
                throw new NotSupportedException();
        }
        ResolveResult(result);
    }

    public void CheckFloat(float value)
    {
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
                throw new NotSupportedException();
        }
        ResolveResult(result);
    }

    public void CheckString(string value)
    {
        var result = value == text;
        ResolveResult(result);
    }
}