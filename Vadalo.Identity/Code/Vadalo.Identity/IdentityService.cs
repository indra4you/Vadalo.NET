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

    public async Task Invite(
        InviteByEmailAddressRequest inviteByEmailAddressRequest
    )
    {
        inviteByEmailAddressRequest
            .ValidateAndThrow();

        var identityOfEdge = await this._identityDataProvider
            .FetchIdentityOfEdgeByEmailAddress(
                inviteByEmailAddressRequest.InvitedByEmailAddress
            );
        if (null == identityOfEdge)
            throw new IdentityOfEdgeNotFoundException(
                $"Invitee with Email Address '{inviteByEmailAddressRequest.InviteeEmailAddress}' not found"
            );

        var identityNode = await this._identityDataProvider
            .FetchIdentityNodeBySignInID(
                inviteByEmailAddressRequest.InviteeEmailAddress
            );
        if (null == identityNode)
            await this._identityDataProvider
                .CreateIdentityNode(
                    identityOfEdge!.FromNode.ID,
                    inviteByEmailAddressRequest.InviteeEmailAddress
                );

        await this._emailNotificationProvider
            .SendInvitation(
                new(
                    inviteByEmailAddressRequest.InviteeEmailAddress,
                    identityOfEdge.ToNode.ToDisplayName()
                )
            );
    }

    public async Task SendOneTimePassword(
        SendOneTimePasswordRequest sendOneTimePasswordRequest
    )
    {
        sendOneTimePasswordRequest
            .ValidateAndThrow();

        var identityNode = await this._identityDataProvider
            .FetchIdentityNodeBySignInID(
                sendOneTimePasswordRequest.EmailAddress!
            );
        if (null == identityNode)
            throw new IdentityNodeNotFoundException(
                $"Identity with Email Address '{sendOneTimePasswordRequest.EmailAddress}' not found"
            );

        var (oneTimePassword, passwordHash) = this._passwordProvider
            .GeneratePassword();

        await this._identityDataProvider
            .CreatePassHashNode(
                identityNode.ID,
                passwordHash
            );

        await this._emailNotificationProvider
            .SendOneTimePassword(
                new(
                    sendOneTimePasswordRequest.EmailAddress!,
                    oneTimePassword
                )
            );
    }

    public async Task<bool> ValidateOneTimePassword(
        ValidateOneTimePasswordRequest validateOneTimePasswordRequest
    )
    {
        validateOneTimePasswordRequest
            .ValidateAndThrow();

        var identityNode = await this._identityDataProvider
            .FetchIdentityNodeBySignInID(
                validateOneTimePasswordRequest.EmailAddress!
            );
        if (null == identityNode)
            throw new IdentityNodeNotFoundException(
                $"Identity with Email Address '{validateOneTimePasswordRequest.EmailAddress}' not found"
            );

        var passHashNode = await this._identityDataProvider
            .FetchActivePassHashNodeByIdentityID(
                identityNode.ID
            );
        if (null == passHashNode)
            return false;

        var isOneTimePasswordValid = this._passwordProvider
            .VerifyPassword(
                passHashNode.PassHash,
                validateOneTimePasswordRequest.OneTimePassword!
            );
        if (isOneTimePasswordValid)
            await this._identityDataProvider
                .UpdatePassHashEdgeByIdentityID(
                    identityNode.ID
                );

        return isOneTimePasswordValid;
    }

    public async Task<string> GenerateAuthenticationToken(
        GenerateAuthenticationTokenRequest generateAuthenticationTokenRequest
    )
    {
        generateAuthenticationTokenRequest
            .ValidateAndThrow();

        var identityOfEdge = await this._identityDataProvider
            .FetchIdentityOfEdgeByEmailAddress(
                generateAuthenticationTokenRequest.EmailAddress!
            );
        if (null == identityOfEdge)
            throw new IdentityOfEdgeNotFoundException(
                $"Identity with Email Address '{generateAuthenticationTokenRequest.EmailAddress}' not found"
            );

        var jwtToken = this._passwordProvider
            .GenerateJwtToken(
                identityOfEdge
            );

        return jwtToken;
    }
}