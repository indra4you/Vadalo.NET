using System;
using System.Collections.Generic;
using System.Linq;

namespace Vadalo.HealthCheck;

public sealed class HealthReport(
    IEnumerable<HealthReportEntry> entries,
    HealthStatus status,
    TimeSpan totalDuration
)
{
    public IReadOnlyList<HealthReportEntry> Entries { get; } = entries.ToList();

    public HealthStatus Status { get; } = status;

    public string StatusDescription { get; } = status.ToString();

    public TimeSpan TotalDuration { get; } = totalDuration;
}