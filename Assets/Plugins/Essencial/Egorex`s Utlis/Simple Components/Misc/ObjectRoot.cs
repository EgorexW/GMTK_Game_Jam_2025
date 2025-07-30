using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectRoot : MonoBehaviour
{
    [ShowInInspector] List<Component> componentsList = new(); // List of Component
    
    Dictionary<Type, object> components = new();
    
    public T GetRootComponent<T>()
    {
        if (components.TryGetValue(typeof(T), out var component)) return (T)component;
        component = componentsList.Find(componentTmp => componentTmp is T);
        if (component != null)
        {
            components[typeof(T)] = component;
        }
        else{
            components[typeof(T)] = GetComponentInChildren<T>();
        }
        return (T)components[typeof(T)];
    }
}