namespace Vadalo.Notification;

public sealed class EmailNotificationToServerOptions(
) : EmailNotificationBaseOptions(
)
{
    public string? SmtpHost { get; set; }

    public ushort? SmtpPort { get; set; }

    public bool? EnableSSL { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}