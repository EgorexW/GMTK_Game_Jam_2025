using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable] public class ObjectWithValue { }

[Serializable]
public class ObjectWithValue<T> : ObjectWithValue, IComparable<ObjectWithValue<T>>
{
    public float value = 1;
    public T Object;

    public ObjectWithValue(float value, T Object)
    {
        this.value = value;
        this.Object = Object;
    }

    public ObjectWithValue(ObjectWithValue<T> obj)
    {
        value = obj.value;
        Object = obj.Object;
    }

    public int CompareTo(ObjectWithValue<T> other)
    {
        if (other == null){
            return 1;
        }
        return Mathf.RoundToInt(10 * (value - other.value));
    }

    // conversion operators
    public static implicit operator T(ObjectWithValue<T> o)
    {
        return o.Object;
    }

    // for if statements
    public static implicit operator float(ObjectWithValue<T> o)
    {
        return o.value;
    }

    public static explicit operator int(ObjectWithValue<T> o)
    {
        return (int)o.value;
    }

    public static ObjectWithValue<T>[] CopyArray(ObjectWithValue<T>[] array)
    {
        var list = new List<ObjectWithValue<T>>();
        foreach (var item in array) list.Add(new ObjectWithValue<T>(item));
        return list.ToArray();
    }

    // equal operators
    public static bool operator ==(ObjectWithValue<T> lhs, ObjectWithValue<T> rhs)
    {
        if (lhs is null){
            return rhs is null;
        }
        if (rhs is null){
            return false;
        }
        if (lhs.Object is null){
            if (rhs.Object is null){
                // null == null = true.
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles the case of null on right side.
        return lhs.Object.Equals(rhs.Object);
    }

    public static bool operator !=(ObjectWithValue<T> lhs, ObjectWithValue<T> rhs)
    {
        return !(lhs == rhs);
    }

    public static ObjectWithValue<T> operator +(ObjectWithValue<T> a, float b)
    {
        a.value += b;
        return a;
    }

    public static ObjectWithValue<T> operator -(ObjectWithValue<T> a, float b)
    {
        return a + -b;
    }

    public override bool Equals(object obj)
    {
        // return base.Equals(obj);
        return Object.Equals(obj);
    }

    public override int GetHashCode()
    {
        // return base.GetHashCode();
        return Object.GetHashCode();
    }


    public override string ToString()
    {
        return Object.ToString() + value;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ObjectWithValue), true)]
class ObjectWithValueDrawer : PropertyDrawer
{
    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label
    )
    {
        var objectProperty = property.FindPropertyRelative("Object");
        var scoreProperty = property.FindPropertyRelative("value");

        position.width -= 45;
        EditorGUI.PropertyField(position, objectProperty, label, true);

        position.x += position.width + 40;
        position.width = 40;
        position.x -= position.width - 5;
        position.height = EditorGUI.GetPropertyHeight(scoreProperty);
        EditorGUI.PropertyField(position, scoreProperty, GUIContent.none);
    }
}
#endif