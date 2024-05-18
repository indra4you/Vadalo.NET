using System;
using System.Threading.Tasks;

namespace Vadalo.Identity.Providers;

public interface IIdentityDataProvider
{
    Task<IdentityOfEdgeModel?> FetchIdentityOfEdgeByEmailAddress(
        string emailAddress
    );

    Task<IdentityNodeModel?> FetchIdentityNodeBySignInID(
        string signInID
    );

    Task CreateIdentityNode(
        Guid invitedBy,
        string signInID
    );

    Task CreatePassHashNode(
        Guid identityID,
        string passHash
    );

    Task<PassHashNodeModel?> FetchActivePassHashNodeByIdentityID(
        Guid identityID
    );

    Task UpdatePassHashEdgeByIdentityID(
        Guid identityID
    );
}