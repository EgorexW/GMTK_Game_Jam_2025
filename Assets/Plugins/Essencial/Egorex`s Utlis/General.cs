using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Flags] 
public enum Direction{
    Left = 1 << 0,
    Right = 1 << 1,
    Up = 1 << 2,
    Down = 1 << 3
}

public class General : MonoBehaviour
{
    static General instance;

    static General GetInstance(){
        if (instance == null){
            instance = new GameObject("General").AddComponent<General>();
        }
        return instance;
    }
    public static void StartAfterSeconds(MonoBehaviour monoBehaviour, IEnumerator coroutine, float seconds){
        GetInstance().StartCoroutine(StartAfterSecondsCoroutine(monoBehaviour, coroutine, seconds));
    }
    public static void CallAfterSeconds(UnityAction action, float seconds = 0){
        GetInstance().StartCoroutine(CallAfterSecondsCoroutine(action, seconds));
    }
    public static Vector2 DirToVector(Direction direction){
        switch (direction){
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
        }
        return Vector2.zero;
    }
    public static List<Vector2> Get4MainDirections2D(){
        return new List<Vector2>{
            Vector2.right,
            Vector2.left,
            Vector2.up,
            Vector2.down
        };
    }
    public static List<Vector3> Get4MainDirections3D(){
        return new List<Vector3>{
            Vector3.right,
            Vector3.left,
            Vector3.forward,
            Vector3.back
        };
    }
    public static float GetAngleFromVector(Vector2 dir){
        dir = dir.normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0){
            angle += 360;
        }
        return angle;
    }
    
    public static Vector3 GetMousePos()
    {
        return GetMouseWorldPos(Input.mousePosition);
    }

    public static Vector3 GetMouseWorldPos(Vector3 mousePos)
    {
        Debug.Assert(Camera.main != null, "Camera.main == null");
        var pos = Camera.main.ScreenToWorldPoint(mousePos);
        pos.z = 0;
        return pos;
    }

    public static Vector2 RandomPointOnCircle(){
        var angle = Random.value * 2 * Mathf.PI;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
    }
    static IEnumerator StartAfterSecondsCoroutine(MonoBehaviour monoBehaviour, IEnumerator coroutine, float seconds){
        yield return new WaitForSeconds(seconds);
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy){
            yield break;
        }
        monoBehaviour.StartCoroutine(coroutine);
    }
    static IEnumerator CallAfterSecondsCoroutine(UnityAction action, float seconds){
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
    public static string TimeToString(float nr){
        var minutes = Mathf.FloorToInt(nr/60);
        var seconds = nr - minutes * 60;
        seconds = Mathf.Round(seconds);
        var text = minutes + ":" + seconds;
        if (seconds < 10){
            text = minutes + ":0" + seconds; 
        }
        return text;
    }
    public static Vector2Int RoundVector(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }

    public static TComponent GetComponentFromCollider<TComponent>(Collider2D collider){
        if (collider == null){
            return default;
        }
        return !collider.TryGetComponent(out TComponent component2) ? default : component2;
    }

    static GameObject GetGameObjectFromCollider(Component collider)
    {
        return collider == null ? null : collider.gameObject;
    }

    public static void WorldText(string text, Vector2 pos, float size, float time = 0.01f){
        WorldText(text, pos, size, time, Color.white);
    }
    public static void WorldText(string text, Vector2 pos, float size, float time, Color color)
    {
        var gameObject = new GameObject("WorldText"){
            transform ={
                position = pos
            },
            hideFlags = HideFlags.HideInHierarchy
        };
        var textMeshPro = gameObject.AddComponent<TextMeshPro>();
        textMeshPro.color = color;
        textMeshPro.text = text;
        textMeshPro.fontSize = size;
        textMeshPro.alignment = TextAlignmentOptions.Center;
        Destroy(gameObject, time);
    }
    public static TComponent GetRootComponent<TComponent>(GameObject gameObject, bool mustBeFound = true)
    {
        return GetRootComponent<TComponent>(gameObject.transform, mustBeFound);
    }
    public static TComponent GetRootComponent<TComponent>(Transform transform, bool mustBeFound = true)
    {
        var objectRoot = GetObjectRoot(transform, false);
        if (objectRoot == null){
            var localComponent = transform.GetComponent<TComponent>();
            Debug.Assert(!mustBeFound || localComponent != null, typeof(TComponent) + " is null", transform);
            return localComponent;
        }
        var component = objectRoot.GetRootComponent<TComponent>();
        Debug.Assert(!mustBeFound || component != null, typeof(TComponent) + " is null", transform);
        return component;
    }

    public static ObjectRoot GetObjectRoot(Transform transform, bool mustBeFound = true)
    {
        var checkedTransform = transform;
        ObjectRoot objectRoot;
        while (true){
            if (checkedTransform.TryGetComponent(out objectRoot)){
                break;
            }
            checkedTransform = checkedTransform.parent;
            if (checkedTransform != null) continue;
            if (mustBeFound){
                Debug.LogError("Object Root not found", transform);
            }
            break;
        }
        return objectRoot;
    }

    public static Vector2 GetClosestPos(Vector2 centerPos, List<Vector2> poses)
    {
        var closestPos = Vector2.zero;
        var smallestDis = Mathf.Infinity;
        foreach (var pos in poses){
            var dis = Vector2.Distance(centerPos, pos);
            if (dis < smallestDis){
                smallestDis = dis;
                closestPos = pos;
            }
        }
        return closestPos;
    }

    public static Vector2 GetClosestPos(Vector3 transformPosition, List<GameObject> enemies)
    {
        var poses = enemies.ConvertAll(input => (Vector2)input.transform.position);
        return GetClosestPos(transformPosition, poses);
    }

    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static float RandomRange(Vector2 vector)
    {
        return UnityEngine.Random.Range(vector.x, vector.y);
    }
    
    public static int RandomRange(Vector2Int vector)
    {
        return UnityEngine.Random.Range(vector.x, vector.y);
    }

    public static HashSet<T> GetUniqueRootComponents<T>(Collider[] colliders)
    {
        var components = new HashSet<T>();
        foreach (var obj in colliders){
            var component = GetRootComponent<T>(obj.gameObject, false);
            if (component != null){
                components.Add(component);
            }
        }
        return components;
    }
    public static Quaternion RotateLeftOrRight(Quaternion rotation, Quaternion targetRotation, float delta)
    {
        var flatDefaultRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        return Quaternion.RotateTowards(rotation, flatDefaultRotation, delta);
    }
}

public interface INamed
{
    public string GetName();
}