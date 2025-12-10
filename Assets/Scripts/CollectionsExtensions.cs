using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class CollectionsExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        return !source.Any();
    }
        
    public static T GetRandom<T>(this IEnumerable<T> source)
    {
        List<T> sourceAsList = source.ToList();
        return sourceAsList[Random.Range(0, sourceAsList.Count)];
    }
        
    public static T GetRandomWeighted<T>(this List<T> source, Func<T, int> fieldSelector)
    {
        var randomValue = Random.Range(0, source.Sum(fieldSelector));

        foreach (var element in source)
        {
            randomValue -= fieldSelector(element);

            if (randomValue >= 0)
                continue;

            return element;
        }

        throw new InvalidOperationException();
    }
        
    public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> values)
    {
        return values.All(source.Contains);
    }
        
    public static bool ContainsAny<T>(this List<T> source, List<T> values)
    {
        return source.Intersect(values).Any();
    }
        
    public static string ToDisplayString<T>(this IEnumerable<T> source)
    {
        if (source == null)
            return "null";

        var s = "[";
        s = source.Aggregate(s, (res, x) => res + x + ", ");

        if (s.Contains(", "))
            s = s.Substring(0, s.Length - 2);

        return $"{s}]";
    }
}