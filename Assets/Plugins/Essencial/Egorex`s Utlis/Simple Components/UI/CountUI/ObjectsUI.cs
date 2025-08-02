using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ObjectsUI : CountUI
{
    [SerializeField] GameObject prefab;

    [FoldoutGroup("Events")] public UnityEvent<GameObject> onCreateObject = new();

    readonly List<GameObject> activeObjs = new();
    readonly Queue<GameObject> inactiveObjs = new();


    // [FoldoutGroup("Events")][SerializeField] UnityEvent onShow;
    // [FoldoutGroup("Events")][SerializeField] UnityEvent onHide;
    void Awake()
    {
        SetPrefab();
        prefab.SetActive(false);
    }

    void OnValidate()
    {
        SetPrefab();
    }

    void SetPrefab()
    {
        if (prefab != null){
            return;
        }
        if (transform.childCount <= 0){
            return;
        }
        prefab = transform.GetChild(0).gameObject;
    }

    public override void SetCount(int count)
    {
        base.SetCount(count);
        SetObjectsCount(count);
    }

    void SetObjectsCount(int count)
    {
        while (activeObjs.Count > count) RemoveObject();
        while (activeObjs.Count < count) AddObject();
    }

    public GameObject AddObject()
    {
        if (inactiveObjs.Count < 1){
            CreateObjectUI();
        }
        var obj = inactiveObjs.Dequeue();
        obj.SetActive(true);
        activeObjs.Add(obj);
        return obj;
    }

    public void RemoveObject(GameObject obj = null)
    {
        var iconUI = activeObjs[^1];
        if (obj == null){
            obj = activeObjs[^1];
        }
        if (obj == null || !activeObjs.Contains(obj)){
            return;
        }
        obj.SetActive(false);
        activeObjs.Remove(obj);
        inactiveObjs.Enqueue(obj);
    }

    public void Clear()
    {
        SetCount(0);
    }

    public List<GameObject> GetActiveObjs()
    {
        return new List<GameObject>(activeObjs);
    }

    protected virtual GameObject CreateObjectUI()
    {
        var newObj = Instantiate(prefab, transform);
        inactiveObjs.Enqueue(newObj);
        onCreateObject.Invoke(newObj);
        return newObj;
    }

    #region Show/Hide

    public void Hide()
    {
        // onHide.Invoke();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // onShow.Invoke();
        gameObject.SetActive(true);
    }

    #endregion
}