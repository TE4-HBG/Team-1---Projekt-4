using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public static bool HasLayer(this Collider collider, int layer)
    {
        return collider.gameObject.layer == layer;
    }
    
}
