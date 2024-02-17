using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck;

public interface IHealthCheck
{
    string Name { get; }

    Task<Models.HealthCheckResult> CheckHealth(
        CancellationToken cancellationToken = default
    );
}