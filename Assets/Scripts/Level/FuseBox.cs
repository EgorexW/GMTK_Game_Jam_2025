using System.Collections.Generic;
using UnityEngine;

public class FuseBox : Box
{
    [SerializeField] List<GameObject> objects;

    public override void DeactivateBox()
    {
        base.DeactivateBox();
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    public override void ActivateBox()
    {
        base.ActivateBox();
        foreach (GameObject obj in objects){
            obj.SetActive(true);
        }
    }
}
