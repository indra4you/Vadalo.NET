using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.Data;

public sealed class SqlServerHealthCheque(
    SqlServerDataOptions sqlServerDataOptions
) : HealthCheck.IHealthCheck
{
    private readonly SqlServerDataOptions _sqlServerDataOptions = sqlServerDataOptions;
    private readonly string _sqlStatement = "SELECT GETDATE()";

    public string Name => "Sql Server Health Check";

    private SqlConnection CreateConnection() =>
        new(
            this._sqlServerDataOptions.ConnectionString
        );

    public async Task<HealthCheck.HealthCheckResult> CheckHealth(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            using var connection = this
                .CreateConnection();
            await connection
                .OpenAsync(
                    cancellationToken
                );

            using var command = connection
                .CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = this._sqlStatement;

            await command
                .ExecuteNonQueryAsync(
                    cancellationToken
                );

            return this
                .Healthy(
                    "Sql Server Health Check is successful"
                );
        }
        catch (Exception ex)
        {
            return this
                .Unhealthy(
                    "Sql Server Health Check has failed",
                    ex
                );
        }
    }
}