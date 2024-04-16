using System;
using System.Collections.Generic;
using System.Linq;

namespace Vadalo;

public static class Extensions
{
    public static bool HasNoValue(
        this string? value,
        bool trimWhiteSpace = true
    )
    {
        if (null == value)
            return true;

        if (trimWhiteSpace)
            value = value.Trim();

        return string.Empty == value;
    }

    public static bool HasValue(
        this string? value,
        bool trimWhiteSpace = true
    ) =>
        !value.HasNoValue(trimWhiteSpace);

    public static bool HasNoValue(
        this Guid? value
    ) =>
        !value.HasValue ||
        Guid.Empty == value;

    public static bool HasValue(
        this Guid? value
    ) =>
        !value.HasNoValue();

    public static bool HasNoValue(
        this Guid value
    ) =>
        Guid.Empty == value;

    public static bool HasValue(
        this Guid value
    ) =>
        !value.HasNoValue();

    public static bool HasValue<T>(
        this IEnumerable<T>? values
    ) =>
        null != values && values.Any();

    public static bool HasNoValue<T>(
        this IEnumerable<T>? values
    ) =>
        !values.HasValue();

    public static bool IsEmailAddress(
        this string value
    ) =>
        PartialExtensions
            .EmailRegex()
            .IsMatch(
                value
            );

    public static bool IsNotEmailAddress(
        this string value
    ) =>
        !value.IsEmailAddress();
}