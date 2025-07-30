using System;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtentions{
    public static void Shuffle<T>(this List<T> list){
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }
    public static T Random<T>(this List<T> list, bool pop = false){
        if (list.Count < 1){
            return default;
        }
        var i = UnityEngine.Random.Range(0, list.Count);
        var obj = list[i];
        if (pop){
            list.RemoveAt(i);
        }
        return obj;
    }
    public static T WeightedRandom<T, TNr>(this Dictionary<T, TNr> list) where TNr : IConvertible{
        float totalWeight = 0;

        foreach(var weightedChance in list)
        {
            totalWeight += Mathf.Max(Convert.ToSingle(weightedChance.Value), 0);
        }

        var roll = UnityEngine.Random.Range(0, totalWeight);

        foreach(var weightedChance in list)
        {
            if (roll <= Convert.ToSingle(weightedChance.Value)){
                return weightedChance.Key;
            }
            roll -= Convert.ToSingle(weightedChance.Value);
        }
        Debug.LogWarning("Invalid Weights");
        return default;
    }
    public static List<T> Copy<T>(this List<T> list){
        return new List<T>(list);
    }
}