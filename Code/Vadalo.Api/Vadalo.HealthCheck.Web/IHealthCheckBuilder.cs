using Microsoft.Extensions.DependencyInjection;

namespace Vadalo.HealthCheck;

public interface IHealthCheckBuilder
{
    IServiceCollection Services { get; }
}