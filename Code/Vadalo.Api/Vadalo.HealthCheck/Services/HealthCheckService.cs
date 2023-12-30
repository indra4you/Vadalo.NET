using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck.Services;

public class HealthCheckService(
    IEnumerable<IHealthCheck> healthChecks
) : IHealthCheckService
{
    private readonly IEnumerable<IHealthCheck> _healthChecks = healthChecks;

    public async Task<Models.HealthReport> CheckHealth(
        CancellationToken cancellationToken = default
    )
    {
        var healthReportEntries = await this.PerformHealthChecks(
            cancellationToken
        );

        return healthReportEntries
            .ToHealthReport();
    }

    private async Task<IEnumerable<Models.HealthReportEntry>> PerformHealthChecks(
        CancellationToken cancellationToken
    )
    {
        var healthReportEntryTasks = this._healthChecks
            .ForEach(
                healthCheck => Task.Run(
                    async () => await healthCheck
                        .PerformHealthCheck(
                            cancellationToken
                        )
                    )
            );

        return await Task.WhenAll(healthReportEntryTasks);
    }
}