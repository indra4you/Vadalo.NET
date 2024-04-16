using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo;

public static class Extensions
{
    public static HealthCheck.HealthCheckResult Unhealthy(
        this HealthCheck.IHealthCheck _,
        string description,
        Exception? exception = null
    ) =>
        new(
            HealthCheck.HealthStatus.Unhealthy,
            description,
            exception
        );

    public static HealthCheck.HealthCheckResult Degraded(
        this HealthCheck.IHealthCheck _,
        string description,
        Exception? exception = null
    ) =>
        new(
            HealthCheck.HealthStatus.Degraded,
            description,
            exception
        );

    public static HealthCheck.HealthCheckResult Healthy(
        this HealthCheck.IHealthCheck _,
        string description
    ) =>
        new(
            HealthCheck.HealthStatus.Healthy,
            description
        );

    internal static HealthCheck.HealthReportEntry ToHealthReportEntry(
        this HealthCheck.HealthCheckResult healthCheckResult,
        HealthCheck.IHealthCheck healthCheck,
        Stopwatch stopwatch
    ) =>
        new(
            healthCheck.Name,
            healthCheckResult.Status,
            stopwatch.Elapsed,
            healthCheckResult.Description,
            healthCheckResult.Exception
        );

    internal static async Task<HealthCheck.HealthReportEntry> PerformHealthCheck(
        this HealthCheck.IHealthCheck healthCheck,
        CancellationToken cancellationToken
    )
    {
        var stopwatch = Stopwatch.StartNew();

        var healthCheckResult = await healthCheck
            .CheckHealth(
                cancellationToken
            );

        stopwatch
            .Stop();

        return healthCheckResult
            .ToHealthReportEntry(
                healthCheck,
                stopwatch
            );
    }

    internal static HealthCheck.HealthReport ToHealthReport(
        this IEnumerable<HealthCheck.HealthReportEntry> healthReportEntries
    )
    {
        if (healthReportEntries.HasNoValue())
            return new(
                [],
                HealthCheck.HealthStatus.Healthy,
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