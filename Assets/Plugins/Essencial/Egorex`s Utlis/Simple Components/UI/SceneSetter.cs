using Sirenix.OdinInspector;
using UnityEngine;

public class SceneSetter : MonoBehaviour
{
    [SceneObjectsOnly] [SerializeField] string scene;
    [SerializeField] bool updateInEditor = true;

    void Awake()
    {
        GiveScene();
    }

    void OnValidate()
    {
        if (!updateInEditor){
            return;
        }
        GiveScene();
    }

    void GiveScene()
    {
        foreach (var sceneGetter in GetComponentsInChildren<ISceneGetter>()) sceneGetter.GetScene(scene);
    }
}

public interface ISceneGetter
{
    void GetScene(string scene);
}