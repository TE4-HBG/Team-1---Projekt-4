using System.Collections.Generic;
using UnityEngine;

public static class FunnyExtensions
{
    public static T Last<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }
    public static T Last<T>(this T[] array)
    {
        return array[array.LongLength - 1];
    }

    public static Vector3 Divide(this Vector3 vector, Vector3 other)
    {
        return new Vector3(
            vector.x /  other.x, 
            vector.y / other.y, 
            vector.z / other.z);
    }
}