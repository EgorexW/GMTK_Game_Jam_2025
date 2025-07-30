using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonsUI<T> : ObjectsUI
{
    UnityAction<T> callback;

    protected override GameObject CreateObjectUI()
    {
        var newObj = base.CreateObjectUI();
        newObj.GetComponent<ObjectButton<T>>().SetCallback(ButtonClicked);
        return newObj;
    }

    void ButtonClicked(T party)
    {
        callback.Invoke(party);
    }

    public void ShowButtons(List<T> objects, UnityAction<T> callback)
    {
        this.callback = callback;
        SetCount(objects.Count);
        var activeObjs = GetActiveObjs();
        for (var i = 0; i < objects.Count; i++){
            var activeObj = activeObjs[i];
            var button = activeObj.GetComponent<ObjectButton<T>>();
            button.SetObject(objects[i]);
        }
    }
}