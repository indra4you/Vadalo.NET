using Microsoft.Extensions.DependencyInjection;

namespace Vadalo.HealthCheck;

public sealed class HealthCheckBuilder(
    IServiceCollection serviceCollection
) : IHealthCheckBuilder
{
    private readonly IServiceCollection _serviceCollection = serviceCollection;

    public IServiceCollection Services =>
        this._serviceCollection;
}