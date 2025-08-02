using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}