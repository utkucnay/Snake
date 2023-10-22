using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Extensions
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        return source.OrderBy((item) => Random.Range(0, 100));
    }
}