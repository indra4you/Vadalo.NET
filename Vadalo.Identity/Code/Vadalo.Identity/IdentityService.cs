using System.Threading.Tasks;

namespace Vadalo.Identity;

public sealed class IdentityService(
    Providers.IIdentityDataProvider identityDataProvider,
    Providers.IEmailNotificationProvider emailNotificationProvider
)
{
    private readonly Providers.IIdentityDataProvider _identityDataProvider = identityDataProvider;
    private readonly Providers.IEmailNotificationProvider _emailNotificationProvider = emailNotificationProvider;

    public async Task InviteByEmailAddress(
        InviteByEmailAddressRequest inviteByEmailAddressRequest
    )
    {
        inviteByEmailAddressRequest
            .ValidateAndThrow();

        var identityOf = await this._identityDataProvider
            .FetchIdentityOfEdgeByEmailAddress(
                inviteByEmailAddressRequest.InvitedByEmailAddress
            );
        if (null == identityOf)
            throw new IdentityOfEdgeNotFoundException(
                $"Invitee with Email Address '{inviteByEmailAddressRequest.InviteeEmailAddress}' not found"
            );

        var identity = await this._identityDataProvider
            .FetchIdentityNodeBySignInID(
                inviteByEmailAddressRequest.InviteeEmailAddress
            );
        if (null == identity)
            await this._identityDataProvider
                .CreateIdentity(
                    identityOf!.FromNode.ID,
                    inviteByEmailAddressRequest.InviteeEmailAddress
                );

        await this._emailNotificationProvider
            .SendInvitation(
                new(
                    inviteByEmailAddressRequest.InviteeEmailAddress,
                    identityOf.ToNode.ToDisplayName()
                )
            );
    }
}