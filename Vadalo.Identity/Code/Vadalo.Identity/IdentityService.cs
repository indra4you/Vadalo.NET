using System.Threading.Tasks;

namespace Vadalo.Identity;

public sealed class IdentityService(
    Providers.IIdentityDataProvider identityDataProvider,
    Providers.IPasswordProvider passwordProvider,
    Providers.IEmailNotificationProvider emailNotificationProvider
)
{
    private readonly Providers.IIdentityDataProvider _identityDataProvider = identityDataProvider;
    private readonly Providers.IPasswordProvider _passwordProvider = passwordProvider;
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
                .CreateIdentityNode(
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

    public async Task SendOneTimePasswordByEmailAddress(
        OneTimePasswordRequest oneTimePasswordRequest
    )
    {
        oneTimePasswordRequest
            .ValidateAndThrow();

        var identity = await this._identityDataProvider
            .FetchIdentityNodeBySignInID(
                oneTimePasswordRequest.EmailAddress!
            );
        if (null == identity)
            throw new IdentityNodeNotFoundException(
                $"Identity with Email Address '{oneTimePasswordRequest.EmailAddress}' not found"
            );

        var (oneTimePassword, passwordHash) = this._passwordProvider
            .GeneratePassword();

        await this._identityDataProvider
            .CreatePassHashNode(
                identity.ID,
                passwordHash
            );

        await this._emailNotificationProvider
            .SendOneTimePassword(
                new(
                    oneTimePasswordRequest.EmailAddress!,
                    oneTimePassword
                )
            );
    }
}