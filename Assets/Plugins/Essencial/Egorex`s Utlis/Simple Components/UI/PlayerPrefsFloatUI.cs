using UnityEngine;

public class PlayerPrefsFloatUI : TextUI
{
    [SerializeField] string prefName;

    protected override void Awake()
    {
        base.Awake();
        var playerPref = PlayerPrefs.GetFloat(prefName, startValue);
        SetCount(playerPref);
    }
}