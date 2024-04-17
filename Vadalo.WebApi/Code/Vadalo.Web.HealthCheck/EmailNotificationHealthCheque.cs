using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.Web.HealthCheck;

public sealed class EmailNotificationHealthCheque(
    Notification.IEmailNotificationService emailNotificationService
) : Vadalo.HealthCheck.IHealthCheck
{
    private readonly Notification.IEmailNotificationService _emailNotificationService = emailNotificationService;
    private readonly Notification.EmailNotificationRequest _emailNotificationRequest = new(
        ["support@vadalo.com"],
        "Health Check",
        Assembly.GetExecutingAssembly(),
        "HealthCheck",
        new Dictionary<string, string>
        {
            ["ServerTime"] = DateTimeOffset.UtcNow.ToString()
        }
    );

    public string Name => "Email Notification Health Check";

    public async Task<Vadalo.HealthCheck.HealthCheckResult> CheckHealth(
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await this._emailNotificationService
                .SendNotification(
                    this._emailNotificationRequest
                );

            return this
                .Healthy(
                    $"{this.Name} is successful"
                );
        }
        catch (Exception ex)
        {
            return this
                .Unhealthy(
                    $"{this.Name} has failed",
                    ex
                );
        }
    }
}