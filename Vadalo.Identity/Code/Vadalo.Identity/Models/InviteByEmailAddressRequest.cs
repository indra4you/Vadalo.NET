namespace Vadalo.Identity;

public sealed class InviteByEmailAddressRequest(
    string invitedByEmailAddress,
    string inviteeEmailAddress
)
{
    public string InvitedByEmailAddress = invitedByEmailAddress;

    public string InviteeEmailAddress = inviteeEmailAddress;
}