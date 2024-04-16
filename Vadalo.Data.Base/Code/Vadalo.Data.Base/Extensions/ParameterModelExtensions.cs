using System.Collections.Generic;
using System.Data;

namespace Vadalo;

public static class ParameterModelExtensions
{
    public static IList<Data.ParameterModel> AddParameter(
        this IList<Data.ParameterModel> parameters,
        Data.ParameterModel parameter
    )
    {
        parameters
            .Add(
                parameter
            );

        return parameters;
    }

    public static IList<Data.ParameterModel> AddParameter(
        this IList<Data.ParameterModel> parameters,
        string parameterName,
        object parameterValue,
        DbType parameterType
    )
    {
        var parameter = new Data.ParameterModel(
            parameterName,
            parameterValue,
            parameterType
        );

        parameters
            .Add(
                parameter
            );

        return parameters;
    }

    public static IList<Data.ParameterModel> AddParameter(
        this IList<Data.ParameterModel> parameters,
        string parameterName,
        object parameterValue
    )
    {
        var parameter = new Data.ParameterModel(
            parameterName,
            parameterValue
        );

        parameters
            .Add(
                parameter
            );

        return parameters;
    }
}