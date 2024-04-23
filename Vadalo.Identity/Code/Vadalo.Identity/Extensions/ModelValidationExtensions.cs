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
        this OneTimePasswordRequest oneTimePasswordRequest
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                oneTimePasswordRequest,
                nameof(oneTimePasswordRequest)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'OneTimePasswordRequest' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckEmailAddressRequired(
                oneTimePasswordRequest.EmailAddress,
                $"{nameof(oneTimePasswordRequest)}.{nameof(oneTimePasswordRequest.EmailAddress)}"
            );

        if (validationMessages.HasValue())
            throw new ValidationException(
                "'OneTimePasswordRequest' model validation failed, check validation messages for more details",
                validationMessages
            );
    }
}