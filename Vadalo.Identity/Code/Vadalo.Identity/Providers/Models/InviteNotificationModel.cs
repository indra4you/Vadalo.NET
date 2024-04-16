namespace Vadalo.Identity.Providers;

public sealed class InviteNotificationModel(
    string emailAddress,
    string invitedByDisplayName
)
{
    public string EmailAddress = emailAddress;

    public string InvitedByDisplayName = invitedByDisplayName;
}