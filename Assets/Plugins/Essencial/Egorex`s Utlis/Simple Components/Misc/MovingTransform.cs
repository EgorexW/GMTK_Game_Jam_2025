using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MovingTransform : MonoBehaviour
{
    [SerializeField] GameObject moveGameObject;
 
    [SerializeField] List<ObjectWithValue<Transform>> movementPositions = new();
    [SerializeField] float stopTime = 2;
    [SerializeField] float defaultValue = 5;
    
    int currentIndex;

    void Awake()
    {
        MoveNext();
    }
    void MoveNext()
    {
        currentIndex++;
        if (currentIndex >= movementPositions.Count){
            currentIndex = 0;
        }
        var time = movementPositions[currentIndex].value;
        var position = movementPositions[currentIndex].Object.position;
        LeanTween.move(moveGameObject, position, time);
        General.CallAfterSeconds(MoveNext, time + stopTime);
    }

    [Button]
    void AddChildrenTransform()
    {
        foreach (var child in GetComponentsInChildren<Transform>()){
            if (child == transform){
                continue;
            }
            if (child.gameObject == moveGameObject){
                continue;
            }
            if (movementPositions.Find(obj => obj.Object == child) != null){
                continue;
            }
            movementPositions.Add(new(defaultValue, child));
        }
    }

    void Reset()
    {
        var child = transform.GetChild(0);
        if (child == null){
            return;
        }
        moveGameObject = child.gameObject;
    }
}
