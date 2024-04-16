using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Vadalo.Data;

public sealed class SqlServerDataProvider(
    SqlServerDataOptions sqlServerDataOptions
) : IDataProvider
{
    private readonly SqlServerDataOptions _sqlServerDataOptions = sqlServerDataOptions;

    private SqlConnection CreateConnection() =>
        new(
            this._sqlServerDataOptions.ConnectionString
        );

    public async Task<int> ExecuteNonQuery(
        string sqlStatement,
        IEnumerable<ParameterModel> parameters
    )
    {
        using var connection = this.CreateConnection();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = sqlStatement;

        command.AddParameters(parameters);

        return await command.ExecuteNonQueryAsync();
    }

    public async Task<int> ExecuteNonQuery(
        string sqlStatement
    ) =>
        await this.ExecuteNonQuery(
            sqlStatement,
            []
        );

    public async Task<object?> ExecuteScalar(
        string sqlStatement,
        IEnumerable<ParameterModel> parameters
    )
    {
        using var connection = this.CreateConnection();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = sqlStatement;

        command.AddParameters(parameters);

        return await command.ExecuteScalarAsync();
    }

    public async Task<object?> ExecuteScalar(
        string sqlStatement
    ) =>
        await this.ExecuteScalar(
            sqlStatement,
            []
        );

    public async Task<IEnumerable<T>> ExecuteReader<T>(
        string sqlStatement,
        IEnumerable<ParameterModel> parameters,
        Func<DbDataReader, T> modelMapperMethod
    )
    {
        using var connection = this.CreateConnection();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = sqlStatement;

        command.AddParameters(parameters);

        var reader = await command.ExecuteReaderAsync();
        var result = new List<T>();

        while (await reader.ReadAsync())
            result.Add(modelMapperMethod(reader));

        return result;
    }

    public async Task<IEnumerable<T>> ExecuteReader<T>(
        string sqlStatement,
        Func<DbDataReader, T> modelMapperMethod
    ) =>
        await this.ExecuteReader(
            sqlStatement,
            [],
            modelMapperMethod
        );
}