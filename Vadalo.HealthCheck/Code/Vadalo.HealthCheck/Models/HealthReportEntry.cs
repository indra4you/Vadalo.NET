using System;

namespace Vadalo.HealthCheck;

public sealed class HealthReportEntry(
    string name,
    HealthStatus status,
    TimeSpan duration,
    string description,
    Exception? exception
)
{
    public string Name { get; } = name;

    public HealthStatus Status { get; } = status;

    public string StatusDescription { get; } = status.ToString();

    public TimeSpan Duration { get; } = duration;

    public string Description { get; } = description;

    public Exception? Exception { get; } = exception;
}