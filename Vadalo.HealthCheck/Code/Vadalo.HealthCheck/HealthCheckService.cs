using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck;

public class HealthCheckService(
    IEnumerable<IHealthCheck> healthChecks
) : IHealthCheckService
{
    private readonly IEnumerable<IHealthCheck> _healthChecks = healthChecks;

    public async Task<HealthReport> CheckHealth(
        CancellationToken cancellationToken = default
    )
    {
        var healthReportEntries = await this.PerformHealthChecks(
            cancellationToken
        );

        return healthReportEntries
            .ToHealthReport();
    }

    private async Task<IEnumerable<HealthReportEntry>> PerformHealthChecks(
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

        var healthReportEnties = await Task
            .WhenAll(
                healthReportEntryTasks
            );

        return healthReportEnties;
    }
}