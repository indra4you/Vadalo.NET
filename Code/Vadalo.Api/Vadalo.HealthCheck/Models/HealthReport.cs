using System;
using System.Collections.Generic;
using System.Linq;

namespace Vadalo.HealthCheck.Models;

public sealed class HealthReport
{
    public HealthReport(
        IEnumerable<HealthReportEntry> entries,
        HealthStatus status,
        TimeSpan totalDuration
    )
    {
        this.Entries = entries.ToList();
        this.Status = status;
        this.TotalDuration = totalDuration;

        this.StatusDescription = this.Status.ToString();
    }

    public IReadOnlyList<HealthReportEntry> Entries { get; private set; }

    public HealthStatus Status { get; private set; }

    public string StatusDescription { get; private set; }

    public TimeSpan TotalDuration { get; private set; }
}