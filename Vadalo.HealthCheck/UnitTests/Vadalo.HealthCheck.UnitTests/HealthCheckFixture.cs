using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck;

public sealed class HealthCheckFixture(
    string name,
    HealthCheckResult healthCheckResult,
    int sleepForInSeconds = 1
) : IHealthCheck
{
    private readonly string _name = name;
    private readonly HealthCheckResult _healthCheckResult = healthCheckResult;

    public string Name => this._name;

    public Task<HealthCheckResult> CheckHealth(
        CancellationToken cancellationToken = default
    )
    {
        Thread
            .Sleep(
                1000 * sleepForInSeconds
            );

        return Task
            .FromResult(
                this._healthCheckResult
            );
    }
}