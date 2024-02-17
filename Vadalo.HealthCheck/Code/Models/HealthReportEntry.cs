using System;

namespace Vadalo.HealthCheck.Models;

public sealed class HealthReportEntry
{
    public HealthReportEntry(
        string name,
        HealthStatus status,
        TimeSpan duration,
        string description,
        Exception? exception
    )
    {
        this.Name = name;
        this.Status = status;
        this.Duration = duration;
        this.Description = description;
        this.Exception = exception;

        this.StatusDescription = this.Status.ToString();
    }

    public string Name { get; private set; }

    public HealthStatus Status { get; private set; }

    public string StatusDescription { get; private set; }

    public TimeSpan Duration { get; private set; }

    public string Description { get; private set; }

    public Exception? Exception { get; private set; }
}