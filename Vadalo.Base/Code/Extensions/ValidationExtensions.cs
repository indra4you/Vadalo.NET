using System.Collections.Generic;
using System.Linq;

namespace Vadalo;

public static class ValidationExtensions
{
    public static bool HaveValue(
        this string? value,
        bool trimWhiteSpace = true
    )
    {
        if (null == value)
            return false;

        if (trimWhiteSpace)
            value = value.Trim();

        return string.Empty != value;
    }

    public static bool HaveNoValue(
        this string? value,
        bool trimWhiteSpace = true
    ) =>
        !value.HaveValue(trimWhiteSpace);

    public static bool HaveValues<T>(
        this IEnumerable<T>? values
    ) =>
        null != values && values.Any();

    public static bool HaveNoValues<T>(
        this IEnumerable<T>? values
    ) =>
        !values.HaveValues();
}