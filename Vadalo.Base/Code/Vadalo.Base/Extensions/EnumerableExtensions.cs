using System;
using System.Collections.Generic;

namespace Vadalo;

public static class EnumerableExtensions
{
    public static IEnumerable<TResult> ForEach<T, TResult>(
        this IEnumerable<T> values,
        Func<T, TResult> action
    )
    {
        var response = new List<TResult>();

        foreach (var value in values)
        {
            var result = action(
                value
            );

            response
                .Add(
                    result
                );
        }

        return response;
    }
}