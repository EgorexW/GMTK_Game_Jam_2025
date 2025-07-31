using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [SerializeField] List<GameObject> objects;

    bool on = true; 
    
    public void ActivateFuseBox()
    {
        on = true;
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }
    
    public void DeactivateFuseBox()
    {
        on = false;
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    public void ChangeState()
    {
        if (on)
        {
            DeactivateFuseBox();
        }
        else
        {
            ActivateFuseBox();
        }
    }
}
