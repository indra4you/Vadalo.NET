using System;

namespace Vadalo.Identity.Providers;

public sealed class PassHashNodeModel(
    Guid identityID,
    string passHash
)
{
    public Guid IdentityID = identityID;

    public string PassHash = passHash;
}