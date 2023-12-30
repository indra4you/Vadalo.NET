using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck;

public sealed class HealthCheckFixture(
    string name,
    Models.HealthCheckResult healthCheckResult,
    int sleepForInSeconds = 1
) : IHealthCheck
{
    private readonly string _name = name;
    private readonly Models.HealthCheckResult _healthCheckResult = healthCheckResult;

    public string Name => this._name;

    public Task<Models.HealthCheckResult> CheckHealth(
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