using System;
using System.Collections.Generic;

namespace Vadalo;

public static class EnumerableExtensions
{
    public static IEnumerable<TResult> ForEach<T, TResult>(
        this IEnumerable<T> values,
        Func<T, int, TResult> func
    )
    {
        var response = new List<TResult>();
        int index = 0;

        foreach (var value in values)
        {
            var result = func(value, index);

            response.Add(result);

            index++;
        }

        return response;
    }

    public static IEnumerable<TResult> ForEach<T, TResult>(
        this IEnumerable<T> values,
        Func<T, TResult> func
    )
    {
        var response = new List<TResult>();

        foreach (var value in values)
        {
            var result = func(value);

            response.Add(result);
        }

        return response;
    }
}