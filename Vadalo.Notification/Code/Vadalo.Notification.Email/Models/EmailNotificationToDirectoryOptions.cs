namespace Vadalo.Notification;

public sealed class EmailNotificationToDirectoryOptions(
) : EmailNotificationBaseOptions (
)
{
    public string? DirectoryLocation { get; set; }
}