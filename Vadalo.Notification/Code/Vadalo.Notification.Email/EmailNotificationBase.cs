using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Vadalo.Notification;

public abstract class EmailNotificationBase(
    EmailNotificationBaseOptions emailNotificationBaseOptions
) : IEmailNotificationService
{
    private readonly EmailNotificationBaseOptions _emailNotificationBaseOptions = emailNotificationBaseOptions;

    protected abstract Task SendEmail(
        MailMessage mailMessage
    );

    private async Task SendEmail(
        IEnumerable<string> sendTos,
        string emailSubject,
        string emailHtmlBody
    )
    {
        using var mailMessage = new MailMessage();

        mailMessage.From = new(
            this._emailNotificationBaseOptions.FromEmailAddress!
        );

        foreach (var sendTo in sendTos)
            mailMessage
                .To
                .Add(
                    sendTo
                );

        mailMessage.Subject = emailSubject;
        mailMessage.Body = emailHtmlBody;
        mailMessage.IsBodyHtml = true;

        await this.SendEmail(
            mailMessage
        );
    }

    public async Task SendNotification(
        EmailNotificationRequest emailNotificationRequest
    )
    {
        emailNotificationRequest
            .ValidateAndThrow();

        var templateValue = await this.LoadHtmlTemplate(
            emailNotificationRequest.FromAssembly,
            emailNotificationRequest.TemplatePath,
            emailNotificationRequest.TemplateNameWithoutExtension
        );

        templateValue = templateValue
            .Replace(
                "{serverRootUrl}",
                this._emailNotificationBaseOptions.ServerRootUrl
            );

        if (emailNotificationRequest.KeyValues.HasValue())
            foreach (var keyValue in emailNotificationRequest.KeyValues!)
                templateValue = templateValue
                    .Replace(
                        $"{{{keyValue.Key}}}",
                        keyValue.Value
                    );

        await this.SendEmail(
            emailNotificationRequest.SendTo,
            emailNotificationRequest.Subject,
            templateValue
        );
    }
}