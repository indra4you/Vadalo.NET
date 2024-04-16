using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Vadalo;

public static class DbParameterExtensions
{
    public static DbCommand AddParameter(
        this DbCommand command,
        string parameterName,
        object parameterValue,
        DbType parameterType
    )
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = parameterName;
        parameter.Value = parameterValue;
        parameter.DbType = parameterType;

        command
            .Parameters
            .Add(
                parameter
            );

        return command;
    }

    public static DbCommand AddParameter(
        this DbCommand command,
        string parameterName,
        object parameterValue
    )
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = parameterName;
        parameter.Value = parameterValue;

        command
            .Parameters
            .Add(
                parameter
            );

        return command;
    }

    public static DbCommand AddParameter(
        this DbCommand command,
        Data.ParameterModel parameterModel
    ) =>
        parameterModel.DbType.HasValue
            ? command
                .AddParameter(
                    parameterModel.Name,
                    parameterModel.Value,
                    parameterModel.DbType.Value
                )
            : command
                .AddParameter(
                    parameterModel.Name,
                    parameterModel.Value
                );

    public static DbCommand AddParameters(
        this DbCommand command,
        IEnumerable<Data.ParameterModel> parameters
    )
    {
        foreach (var parameterModel in parameters)
            command.AddParameter(parameterModel);

        return command;
    }
}