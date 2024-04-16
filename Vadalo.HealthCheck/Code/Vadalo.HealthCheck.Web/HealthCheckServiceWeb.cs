using Microsoft.Extensions.DependencyInjection;
using System;

namespace Vadalo.HealthCheck;

public sealed class HealthCheckServiceWeb(
    IServiceProvider serviceProvider
) : HealthCheckService(
    serviceProvider.GetServices<IHealthCheck>()
)
{ }