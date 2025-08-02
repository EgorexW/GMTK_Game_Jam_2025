using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] TextMeshProUGUI text;

    [SerializeField] List<string> messages;

    [SerializeField] List<GameObject> triggers;

    [SerializeField] float endDelay = 3f;
    [SerializeField] string sceneName = "Normal";
    int index = -1;

    void Awake()
    {
        foreach (var trigger in triggers) trigger.SetActive(false);
    }

    void Start()
    {
        TriggerTriggered();
    }

    public void TriggerTriggered()
    {
        if (index >= 0){
            triggers[index].SetActive(false);
        }
        index++;
        if (index >= messages.Count){
            text.text = "Tutorial completed!";
            General.CallAfterSeconds(() => { SceneManager.LoadScene(sceneName); }, endDelay);
            return;
        }
        text.text = messages[index];
        triggers[index].SetActive(true);
    }
}