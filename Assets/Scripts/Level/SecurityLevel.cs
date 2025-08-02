using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class SecurityLevel : MonoBehaviour
{
    [FormerlySerializedAs("gameManager")]
    [FormerlySerializedAs("ai")]
    [BoxGroup("References")]
    [Required]
    [SerializeField]
    RoundManager roundManager;

    [BoxGroup("References")] [Required] [SerializeField] List<SecurityBox> securityBoxes;
    [BoxGroup("References")] [Required] [SerializeField] List<float> warningLevels;
    [BoxGroup("References")] [Required] [SerializeField] List<GameObject> warningLights;

    [SerializeField] float maxLevel = 60;
    [SerializeField] float baseChange = -6;
    [SerializeField] float changePerOnBox = 2;

    float currentLevel;

    void Start()
    {
        currentLevel = maxLevel;
    }

    void Update()
    {
        var onBoxes = 0;
        foreach (var box in securityBoxes) onBoxes += box.On ? 1 : 0;
        var change = baseChange + onBoxes * changePerOnBox;
        currentLevel += change * Time.deltaTime;
        currentLevel = Mathf.Clamp(currentLevel, -1, maxLevel);
        foreach (var warningLevel in warningLevels)
            warningLights[warningLevels.IndexOf(warningLevel)].SetActive(currentLevel < warningLevel);
        // Debug.Log("Security Level: " + currentLevel);
        if (currentLevel > 0){
            return;
        }
        Debug.Log("Security Level reached zero!");
        roundManager.GameLost();
    }
}