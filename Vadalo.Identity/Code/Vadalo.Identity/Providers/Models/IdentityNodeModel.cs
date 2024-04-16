using System;

namespace Vadalo.Identity.Providers;

public sealed class IdentityNodeModel(
    Guid id,
    Guid invitedBy,
    string signInID
)
{
    public Guid ID = id;

    public Guid InvitedBy = invitedBy;

    public string SignInID = signInID;
}