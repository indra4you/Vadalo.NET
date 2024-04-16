using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Vadalo.Notification;

public sealed class EmailNotificationToServerService : EmailNotificationBase
{
    private readonly EmailNotificationToServerOptions _emailNotificationToServerOptions;

    public EmailNotificationToServerService(
        EmailNotificationToServerOptions emailNotificationToServerOptions
    ) : base(
        emailNotificationToServerOptions
    )
    {
        emailNotificationToServerOptions
            .ValidateAndThrow();

        this._emailNotificationToServerOptions = emailNotificationToServerOptions;
    }

    protected override async Task SendEmail(
        MailMessage mailMessage
    )
    {
        using var smtpClient = new SmtpClient();

        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Host = this._emailNotificationToServerOptions.SmtpHost!;
        smtpClient.Port = this._emailNotificationToServerOptions.SmtpPort!.Value;
        smtpClient.EnableSsl = this._emailNotificationToServerOptions.EnableSSL!.Value;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(
            this._emailNotificationToServerOptions.UserName,
            this._emailNotificationToServerOptions.Password
        );

        await smtpClient
            .SendMailAsync(
                mailMessage
            );
    }
}