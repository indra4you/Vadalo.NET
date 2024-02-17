using Microsoft.Extensions.DependencyInjection;

namespace Vadalo.HealthCheck;

public sealed class HealthCheckServiceWeb(
    IServiceProvider serviceProvider
) : Services.HealthCheckService(
    serviceProvider.GetServices<IHealthCheck>()
)
{
}