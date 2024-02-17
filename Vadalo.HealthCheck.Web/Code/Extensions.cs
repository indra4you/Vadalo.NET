using Microsoft.Extensions.DependencyInjection;

namespace Vadalo;

public static class Extensions
{
    public static HealthCheck.IHealthCheckBuilder AddHealthChecks(
        this IServiceCollection serviceCollection
    )
    {
        var healthCheckBuilder = new HealthCheck.HealthCheckBuilder(serviceCollection);

        serviceCollection
            .AddSingleton<HealthCheck.IHealthCheckBuilder>(healthCheckBuilder);
        serviceCollection
            .AddTransient<HealthCheck.IHealthCheckService, HealthCheck.HealthCheckServiceWeb>();

        return healthCheckBuilder;
    }

    public static HealthCheck.IHealthCheckBuilder AddHealthCheck<TImplementation>(
        this HealthCheck.IHealthCheckBuilder healthCheckBuilder
    ) where TImplementation : class, HealthCheck.IHealthCheck
    {
        healthCheckBuilder.Services
            .AddTransient<HealthCheck.IHealthCheck, TImplementation>();

        return healthCheckBuilder;
    }
}