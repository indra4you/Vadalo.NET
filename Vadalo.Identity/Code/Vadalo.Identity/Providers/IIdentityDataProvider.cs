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

    Task CreateIdentity(
        Guid invitedBy,
        string signInID
    );
}