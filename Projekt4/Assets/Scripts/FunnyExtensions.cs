using System;
using System.Collections.Generic;

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
}
