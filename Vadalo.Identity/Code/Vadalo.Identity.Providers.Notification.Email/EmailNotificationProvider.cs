using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Vadalo.Identity.Providers;

public sealed class EmailNotificationProvider(
    Notification.IEmailNotificationService emailNotificationService
) : IEmailNotificationProvider
{
    private readonly Notification.IEmailNotificationService _emailNotificationService = emailNotificationService;

    public async Task SendInvitation(
        InviteNotificationModel inviteNotificationModel
    )
    {
        var emailNotificationRequest = new Notification.EmailNotificationRequest(
            [inviteNotificationModel.EmailAddress],
            "Welcome to Vadalo",
            Assembly.GetExecutingAssembly(),
            "Invitation",
            new Dictionary<string, string>
            {
                ["InvitedByDisplayName"] = inviteNotificationModel.InvitedByDisplayName,
            }
        );

        await this._emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );
    }
}