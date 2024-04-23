using System.Threading.Tasks;

namespace Vadalo.Identity.Providers;

public interface IEmailNotificationProvider
{
    Task SendInvitation(
        InviteNotificationModel inviteNotificationModel
    );

    Task SendOneTimePassword(
        OneTimePasswordNotificationModel oneTimePasswordNotificationModel
    );
}