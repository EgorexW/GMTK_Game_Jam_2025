using Nrjwolf.Tools.AttachAttributes;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FetchScore : MonoBehaviour
{
    [GetComponent] [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = $"Game Won!\n \n" + 
                         $"Number of loops: {GameplayLoop.i.loopNr}\n" +
                         $"Difficulty: {GameplayLoop.i.difficultyName}";

        GameplayLoop.i.Delete();
    }
}