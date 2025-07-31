using System;
using UnityEngine;

public class SecurityBox : Box
{
    [SerializeField] GameObject obj;
    
    public override void DeactivateBox()
    {
        base.DeactivateBox();
        if (obj != null)
        {
            obj.SetActive(true);
        }
    }

    public override void ActivateBox()
    {
        base.ActivateBox();
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }
}
