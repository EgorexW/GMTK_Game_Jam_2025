using System;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class ShowIfInTypeAttribute : PropertyAttribute
{
    public Type type;

    public ShowIfInTypeAttribute()
    {
        order = 100;
    }
}

public class ShowIfInTypeAttributeDrawer : OdinAttributeDrawer<ShowIfInTypeAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        if (CheckTypeMatch()){
            CallNextDrawer(label);
        }
    }

    bool CheckTypeMatch()
    {
        var objectType = Property.ParentType;

        var declaringType = Attribute.type;

        // Debug.Log($"Parent Type: {objectType}, Declaring Type: {declaringType}, Equality: {objectType == declaringType}");

        return objectType == declaringType;
    }
}