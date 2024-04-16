using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vadalo;

public static class CheckExtensions
{
    public static IList<string> CheckRequired(
        this IList<string> validationMessages,
        object? value,
        string validationPath
    )
    {
        if (null == value)
            validationMessages
                .Add(
                    $"{validationPath}:null"
                );

        return validationMessages;
    }

    public static IList<string> CheckRequired<T>(
        this IList<string> validationMessages,
        IEnumerable<T>? value,
        string validationPath
    )
    {
        if (null == value)
            validationMessages
                .Add(
                    $"{validationPath}:null"
                );
        else if (value.HasNoValue())
            validationMessages
                .Add(
                    $"{validationPath}:empty"
                );

        return validationMessages;
    }

    public static IList<string> CheckRequired(
        this IList<string> validationMessages,
        Assembly? value,
        string validationPath
    )
    {
        if (null == value)
            validationMessages
                .Add(
                    $"{validationPath}:null"
                );

        return validationMessages;
    }

    public static IList<string> CheckRequired(
        this IList<string> validationMessages,
        Guid? value,
        string validationPath
    )
    {
        if (null == value)
            validationMessages
                .Add(
                    $"{validationPath}:null"
                );
        else if (Guid.Empty == value)
            validationMessages
                .Add(
                    $"{validationPath}:empty"
                );

        return validationMessages;
    }

    public static IList<string> CheckRequired(
        this IList<string> validationMessages,
        string? value,
        string validationPath
    )
    {
        if (null == value)
            validationMessages
                .Add(
                    $"{validationPath}:null"
                );
        else if (string.Empty == value)
            validationMessages
                .Add(
                    $"{validationPath}:empty"
                );
        else if (string.Empty == value.Trim())
            validationMessages
                .Add(
                    $"{validationPath}:whitespaces"
                );

        return validationMessages;
    }

    public static IList<string> CheckEmailAddress(
        this IList<string> validationMessages,
        string? value,
        string validationPath
    )
    {
        if (null == value)
            return validationMessages;


        if (value.IsNotEmailAddress())
            validationMessages
                .Add(
                    $"{validationPath}:not-email"
                );

        return validationMessages;
    }

    public static IList<string> CheckEmailAddressRequired(
        this IList<string> validationMessages,
        string? value,
        string validationPath
    )
    {
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );
        if (0 == validationMessages.Count)
            validationMessages
                .CheckEmailAddress(
                    value,
                    validationPath
                );

        return validationMessages;
    }
}