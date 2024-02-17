using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck;

public static class Extensions
{
    public static Models.HealthCheckResult Unhealthy(
        this IHealthCheck _,
        string description,
        Exception? exception = null
    ) =>
        new(
            HealthStatus.Unhealthy,
            description,
            exception
        );

    public static Models.HealthCheckResult Degraded(
        this IHealthCheck _,
        string description,
        Exception? exception = null
    ) =>
        new(
            HealthStatus.Degraded,
            description,
            exception
        );

    public static Models.HealthCheckResult Healthy(
        this IHealthCheck _,
        string description
    ) =>
        new(
            HealthStatus.Healthy,
            description
        );

    internal static Models.HealthReportEntry ToHealthReportEntry(
        this Models.HealthCheckResult healthCheckResult,
        IHealthCheck healthCheck,
        Stopwatch stopwatch
    ) =>
        new(
            healthCheck.Name,
            healthCheckResult.Status,
            stopwatch.Elapsed,
            healthCheckResult.Description,
            healthCheckResult.Exception
        );

    internal static async Task<Models.HealthReportEntry> PerformHealthCheck(
        this IHealthCheck healthCheck,
        CancellationToken cancellationToken
    )
    {
        var stopwatch = Stopwatch.StartNew();

        var healthCheckResult = await healthCheck
            .CheckHealth(
                cancellationToken
            );

        stopwatch.Stop();

        return healthCheckResult
            .ToHealthReportEntry(
                healthCheck,
                stopwatch
            );
    }

    internal static Models.HealthReport ToHealthReport(
        this IEnumerable<Models.HealthReportEntry> healthReportEntries
    )
    {
        if (healthReportEntries.HaveNoValues())
            return new(
                Array.Empty<Models.HealthReportEntry>(),
                HealthStatus.Healthy,
                TimeSpan.Zero
            );

        return new(
            healthReportEntries,
            healthReportEntries
                .Select(s => s.Status)
                .Min(),
            healthReportEntries
                .Select(s => s.Duration)
                .Aggregate(TimeSpan.Zero, (current, next) => current + next)
        );
    }
}