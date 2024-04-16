using System.Collections.Generic;

namespace Vadalo.Notification;

internal static class ModelValidationExtensions
{
    internal static void ValidateAndThrow(
        this EmailNotificationToServerOptions emailNotificationToServerOptions
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                emailNotificationToServerOptions,
                nameof(emailNotificationToServerOptions)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'EmailNotificationToServerOptions' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckRequired(
                emailNotificationToServerOptions.ServerRootUrl,
                $"{nameof(emailNotificationToServerOptions)}.{nameof(emailNotificationToServerOptions.ServerRootUrl)}"
            )
            .CheckRequired(
                emailNotificationToServerOptions.FromEmailAddress,
                $"{nameof(emailNotificationToServerOptions)}.{nameof(emailNotificationToServerOptions.FromEmailAddress)}"
            )
            .CheckRequired(
                emailNotificationToServerOptions.SmtpHost,
                $"{nameof(emailNotificationToServerOptions)}.{nameof(emailNotificationToServerOptions.SmtpHost)}"
            )
            .CheckRequired(
                emailNotificationToServerOptions.SmtpPort,
                $"{nameof(emailNotificationToServerOptions)}.{nameof(emailNotificationToServerOptions.SmtpPort)}"
            )
            .CheckRequired(
                emailNotificationToServerOptions.EnableSSL,
                $"{nameof(emailNotificationToServerOptions)}.{nameof(emailNotificationToServerOptions.EnableSSL)}"
            )
            .CheckRequired(
                emailNotificationToServerOptions.UserName,
                $"{nameof(emailNotificationToServerOptions)}.{nameof(emailNotificationToServerOptions.UserName)}"
            )
            .CheckRequired(
                emailNotificationToServerOptions.Password,
                $"{nameof(emailNotificationToServerOptions)}.{nameof(emailNotificationToServerOptions.Password)}"
            );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'EmailNotificationToServerOptions' model validation failed, check validation messages for more details",
                validationMessages
            );
    }

    internal static void ValidateAndThrow(
        this EmailNotificationToDirectoryOptions emailNotificationToDirectoryOptions
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                emailNotificationToDirectoryOptions,
                nameof(emailNotificationToDirectoryOptions)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'EmailNotificationToDirectoryOptions' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckRequired(
                emailNotificationToDirectoryOptions.ServerRootUrl,
                $"{nameof(emailNotificationToDirectoryOptions)}.{nameof(emailNotificationToDirectoryOptions.ServerRootUrl)}"
            )
            .CheckRequired(
                emailNotificationToDirectoryOptions.FromEmailAddress,
                $"{nameof(emailNotificationToDirectoryOptions)}.{nameof(emailNotificationToDirectoryOptions.FromEmailAddress)}"
            )
            .CheckRequired(
                emailNotificationToDirectoryOptions.DirectoryLocation,
                $"{nameof(emailNotificationToDirectoryOptions)}.{nameof(emailNotificationToDirectoryOptions.DirectoryLocation)}"
            );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'EmailNotificationToDirectoryOptions' model validation failed, check validation messages for more details",
                validationMessages
            );
    }

    internal static void ValidateAndThrow(
        this EmailNotificationRequest emailNotificationRequest
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                emailNotificationRequest,
                nameof(emailNotificationRequest)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'EmailNotificationRequest' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckRequired(
                emailNotificationRequest.SendTo,
                $"{nameof(emailNotificationRequest)}.{nameof(emailNotificationRequest.SendTo)}"
            )
            .CheckRequired(
                emailNotificationRequest.Subject,
                $"{nameof(emailNotificationRequest)}.{nameof(emailNotificationRequest.Subject)}"
            )
            .CheckRequired(
                emailNotificationRequest.FromAssembly,
                $"{nameof(emailNotificationRequest)}.{nameof(emailNotificationRequest.FromAssembly)}"
            )
            .CheckRequired(
                emailNotificationRequest.TemplateNameWithoutExtension,
                $"{nameof(emailNotificationRequest)}.{nameof(emailNotificationRequest.TemplateNameWithoutExtension)}"
            )
            .CheckRequired(
                emailNotificationRequest.TemplatePath,
                $"{nameof(emailNotificationRequest)}.{nameof(emailNotificationRequest.TemplatePath)}"
            );

        if (emailNotificationRequest.SendTo.HasValue())
            foreach (var sendTo in emailNotificationRequest.SendTo)
                validationMessages
                    .CheckEmailAddress(
                        sendTo,
                        $"{nameof(emailNotificationRequest)}.{nameof(emailNotificationRequest.SendTo)}[{sendTo}]"
                    );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'EmailNotificationRequest' model validation failed, check validation messages for more details",
                validationMessages
            );
    }
}