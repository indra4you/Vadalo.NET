using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Vadalo.Notification;

public sealed class EmailNotificationToDirectoryService : EmailNotificationBase
{
    private readonly EmailNotificationToDirectoryOptions _emailNotificationToDirectoryOptions;

    public EmailNotificationToDirectoryService(
        EmailNotificationToDirectoryOptions emailNotificationToDirectoryOptions
    ) : base(
        emailNotificationToDirectoryOptions
    )
    {
        emailNotificationToDirectoryOptions
            .ValidateAndThrow();

        this._emailNotificationToDirectoryOptions = emailNotificationToDirectoryOptions;
    }

    protected override async Task SendEmail(
        MailMessage mailMessage
    )
    {
        using var smtpClient = new SmtpClient();

        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
        smtpClient.PickupDirectoryLocation = Path
            .GetFullPath(
                this._emailNotificationToDirectoryOptions.DirectoryLocation!
            );

        await smtpClient
            .SendMailAsync(
                mailMessage
            );
    }
}