using System.Collections.Generic;

namespace Vadalo.Identity;

internal static class ModelValidationExtensions
{
    internal static void ValidateAndThrow(
        this InviteByEmailAddressRequest inviteByEmailAddressRequest
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                inviteByEmailAddressRequest,
                nameof(inviteByEmailAddressRequest)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'InviteByEmailAddressRequest' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckEmailAddressRequired(
                inviteByEmailAddressRequest.InvitedByEmailAddress,
                $"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InvitedByEmailAddress)}"
            )
            .CheckEmailAddressRequired(
                inviteByEmailAddressRequest.InviteeEmailAddress,
                $"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InviteeEmailAddress)}"
            );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'InviteByEmailAddressRequest' model validation failed, check validation messages for more details",
                validationMessages
            );
    }

    internal static void ValidateAndThrow(
        this SendOneTimePasswordRequest sendOneTimePasswordRequest
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                sendOneTimePasswordRequest,
                nameof(sendOneTimePasswordRequest)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'SendOneTimePasswordRequest' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckEmailAddressRequired(
                sendOneTimePasswordRequest.EmailAddress,
                $"{nameof(sendOneTimePasswordRequest)}.{nameof(sendOneTimePasswordRequest.EmailAddress)}"
            );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'SendOneTimePasswordRequest' model validation failed, check validation messages for more details",
                validationMessages
            );
    }

    internal static void ValidateAndThrow(
        this ValidateOneTimePasswordRequest validateOneTimePasswordRequest
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                validateOneTimePasswordRequest,
                nameof(validateOneTimePasswordRequest)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'ValidateOneTimePasswordRequest' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckEmailAddressRequired(
                validateOneTimePasswordRequest.EmailAddress,
                $"{nameof(validateOneTimePasswordRequest)}.{nameof(validateOneTimePasswordRequest.EmailAddress)}"
            )
            .CheckRequired(
                validateOneTimePasswordRequest.OneTimePassword,
                $"{nameof(validateOneTimePasswordRequest)}.{nameof(validateOneTimePasswordRequest.OneTimePassword)}"
            );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'ValidateOneTimePasswordRequest' model validation failed, check validation messages for more details",
                validationMessages
            );
    }

    internal static void ValidateAndThrow(
        this GenerateAuthenticationTokenRequest generateAuthenticationTokenRequest
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                generateAuthenticationTokenRequest,
                nameof(generateAuthenticationTokenRequest)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'GenerateAuthenticationTokenRequest' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckEmailAddressRequired(
                generateAuthenticationTokenRequest.EmailAddress,
                $"{nameof(generateAuthenticationTokenRequest)}.{nameof(generateAuthenticationTokenRequest.EmailAddress)}"
            );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'GenerateAuthenticationTokenRequest' model validation failed, check validation messages for more details",
                validationMessages
            );
    }
}