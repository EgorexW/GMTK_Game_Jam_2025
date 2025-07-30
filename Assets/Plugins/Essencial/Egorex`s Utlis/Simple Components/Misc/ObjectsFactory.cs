using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ObjectsFactory : MonoBehaviour
{
    [SerializeField] GameObject prefab = null;
    
    List<GameObject> objects = new();

    [FoldoutGroup("Events")] public UnityEvent<GameObject> onCreateObject = new ();
    [FoldoutGroup("Events")] public UnityEvent<GameObject> onRemoveObject = new ();

    void Awake()
    {
        SetPrefab();
        prefab.SetActive(false);
    }

    void SetPrefab()
    {
        if (prefab != null) return;
        if (transform.childCount <= 0) return;
        prefab = transform.GetChild(0).gameObject;
    }
    public void SetCount(int count)
    {
        while(objects.Count > count){
            RemoveObject();
        }
        while(objects.Count < count){
            AddObject();
        }
    }

    public GameObject AddObject()
    {
        var obj = Instantiate(prefab, transform);
        obj.SetActive(true);
        objects.Add(obj);
        onCreateObject.Invoke(obj);
        return obj;
    }
    
    public void RemoveObject(GameObject obj = null){
        if (obj == null){
            if (objects.Count == 0) return;
            obj = objects[^1];
        }
        else if (!objects.Contains(obj)){
            return;
        }
        onRemoveObject.Invoke(obj);
        Destroy(obj);
        objects.Remove(obj);
    }
    public void Clear(){
        SetCount(0);
    }

    public List<GameObject> GetObjects()
    {
        return new(objects);
    }
    void OnValidate()
    {
        SetPrefab();
    }
}
