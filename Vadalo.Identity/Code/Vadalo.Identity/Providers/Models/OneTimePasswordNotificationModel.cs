namespace Vadalo.Identity.Providers;

public sealed class OneTimePasswordNotificationModel(
    string emailAddress,
    string oneTimePassword
)
{
    public string EmailAddress = emailAddress;

    public string OneTimePassword = oneTimePassword;
}