using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.Web.HealthCheck;

public sealed class SqlServerHealthCheque(
    Data.IDataProvider dataProvider
) : Vadalo.HealthCheck.IHealthCheck
{
    private readonly Data.IDataProvider _dataProvider = dataProvider;
    private readonly string _sqlStatement = "SELECT GETDATE()";

    public string Name => "Sql Server Health Check";

    public async Task<Vadalo.HealthCheck.HealthCheckResult> CheckHealth(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await this._dataProvider
                .ExecuteNonQuery(
                    this._sqlStatement
                );

            return this
                .Healthy(
                    $"{this.Name} is successful"
                );
        }
        catch (Exception ex)
        {
            return this
                .Unhealthy(
                    $"{this.Name} has failed",
                    ex
                );
        }
    }
}