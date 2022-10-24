using System.Collections.Generic;

public static class FunnyExtensions
{
    public static T Last<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }
}
