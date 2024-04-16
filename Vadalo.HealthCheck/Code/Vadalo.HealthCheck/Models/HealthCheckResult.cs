using System;

namespace Vadalo.HealthCheck;

public sealed class HealthCheckResult(
    HealthStatus status,
    string description,
    Exception? exception = null
)
{
    public HealthStatus Status { get; private set; } = status;

    public string Description { get; private set; } = description;

    public Exception? Exception { get; private set; } = exception;
}