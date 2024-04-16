namespace Vadalo.Notification;

public abstract class EmailNotificationBaseOptions(
)
{
    public string? ServerRootUrl { get; set; }

    public string? FromEmailAddress { get; set; }
}