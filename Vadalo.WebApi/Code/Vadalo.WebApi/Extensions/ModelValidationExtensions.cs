using System.Collections.Generic;

namespace Vadalo.Web.Api;

internal static class ModelValidationExtensions
{
    internal static void ValidateAndThrow(
        this IdentityInviteRequest identityInviteRequest
    )
    {
        var validationMessages = new List<string>();

        validationMessages
            .CheckRequired(
                identityInviteRequest,
                nameof(identityInviteRequest)
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'IdentityInviteRequest' model validation failed, check validation messages for more details",
                validationMessages
            );

        validationMessages
            .CheckEmailAddressRequired(
                identityInviteRequest.InviteeEmailAddress,
                $"{nameof(identityInviteRequest)}.{nameof(identityInviteRequest.InviteeEmailAddress)}"
            );
        if (validationMessages.HasValue())
            throw new ValidationException(
                "'IdentityInviteRequest' model validation failed, check validation messages for more details",
                validationMessages
            );
    }
}