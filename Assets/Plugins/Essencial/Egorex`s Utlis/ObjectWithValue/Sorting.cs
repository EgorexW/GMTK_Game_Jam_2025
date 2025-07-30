using System.Collections.Generic;

public class Sorting
{ 
    public static ObjectWithValue<T>[] quickSort<T>(ObjectWithValue<T>[] arr)
    {
        if (arr.Length <= 0){
            return new ObjectWithValue<T>[0];
        }
        return SortArray(arr, 0, arr.Length - 1);
    }

    public static List<ObjectWithValue<T>> quickSort<T>(List<ObjectWithValue<T>> list)
    {
        return new List<ObjectWithValue<T>>(quickSort<T>(list.ToArray()));
    }

    public static ObjectWithValue<T>[] SortArray<T>(ObjectWithValue<T>[] array, int leftIndex, int rightIndex)
    {
        var list = new List<ObjectWithValue<T>>(array);
        list.Sort();
        return list.ToArray();
        // var i = leftIndex;
        // var j = rightIndex;
        // var pivot = array[leftIndex].value;
        // while (i <= j)
        // {
        //     while (array[i].value < pivot)
        //     {
        //         i++;
        //     }

        //     while (array[j].value > pivot)
        //     {
        //         j--;
        //     }
        //     if (i <= j)
        //     {
        //         ObjectWithValue<T> temp = array[i];
        //         array[i]= array[j];
        //         array[j] = temp;
        //         i++;
        //         j--;
        //     }
        // }

        // if (leftIndex < j)
        //     SortArray(array, leftIndex, j);
        // if (i < rightIndex)
        //     SortArray(array, i, rightIndex);
        // return array;
    }

    public static ObjectWithValue<T> GetHighiestValue<T>(ObjectWithValue<T>[] values)
    {
        var highiestValue = values[0];
        foreach (var obj in values)
        {          
            if (obj.value > highiestValue.value)
            {
                highiestValue = obj;
            }
        }
        return highiestValue;
    }
}

