using System;
using UnityEngine;

[Serializable]
public struct Optional<T>
{
    [SerializeField] bool enabled;
    [SerializeField] T value;

    public bool Enabled{
        get => enabled;
        set => enabled = value;
    }
    public T Value => value;

    public Optional(T initialValue)
    {
        enabled = true;
        value = initialValue;

        // Value state based toggling approach: 
        // if (value == null) {
        //     enabled = false;
        // } else {
        //     enabled = !value.Equals(default(T));
        // }
    }

    public Optional(T initialValue, bool enabled)
    {
        this.enabled = enabled;
        value = initialValue;
    }

    // conversion operators
    public static implicit operator Optional<T>(T v)
    {
        return new Optional<T>(v);
    }

    public static implicit operator T(Optional<T> o)
    {
        return o.Value;
    }

    // for if statements
    public static implicit operator bool(Optional<T> o)
    {
        return o.enabled;
    }

    // equal operators
    public static bool operator ==(Optional<T> lhs, Optional<T> rhs)
    {
        if (lhs.value is null){
            if (rhs.value is null){
                // null == null = true.
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles the case of null on right side.
        return lhs.value.Equals(rhs.value);
    }

    public static bool operator !=(Optional<T> lhs, Optional<T> rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object obj)
    {
        // return base.Equals(obj);
        return value.Equals(obj);
    }

    public override int GetHashCode()
    {
        // return base.GetHashCode();
        return value.GetHashCode();
    }


    public override string ToString()
    {
        return value.ToString();
    }
}