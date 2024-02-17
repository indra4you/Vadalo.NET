using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck;

public interface IHealthCheckService
{
    Task<Models.HealthReport> CheckHealth(
        CancellationToken cancellationToken = default
    );
}