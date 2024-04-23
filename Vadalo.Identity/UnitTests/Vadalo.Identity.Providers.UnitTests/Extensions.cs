using System;

namespace Vadalo.Identity.Providers;

internal static class Extensions
{
    internal const string INVITEE_EMAIL_ADDRESS = "invitee@domain.com";
    internal const string INVITED_BY_EMAIL_ADDRESS = "invited-by@domain.com";

    internal static IdentityNodeModel MockIdentityNodeModel(
        string? signInID = null,
        Guid? invitedBy = null,
        Guid? id = null
    ) =>
        new(
            id ?? Guid.NewGuid(),
            invitedBy ?? Guid.NewGuid(),
            signInID ?? INVITEE_EMAIL_ADDRESS
        );

    internal static MemberNodeModel MockMemberNodeModel(
        string? emailAddress = null,
        string? lastName = null,
        string? firstName = null,
        byte? status = null,
        Guid? id = null,
        string? middleName = null
    ) =>
        new(
            id ?? Guid.NewGuid(),
            status ?? 0,
            emailAddress ?? INVITED_BY_EMAIL_ADDRESS,
            lastName ?? "Last",
            firstName ?? "First",
            middleName
        );

    internal static string ToDisplayName(
        this MemberNodeModel memberNodeModel
    ) =>
        memberNodeModel.MiddleName.HasNoValue()
            ? string
                .Join(
                    ' ',
                    memberNodeModel.FirstName,
                    memberNodeModel.LastName
                )
            : string
                .Join(
                    ' ',
                    memberNodeModel.FirstName,
                    memberNodeModel.MiddleName,
                    memberNodeModel.LastName
                );

    internal static IdentityOfEdgeModel MockIdentityOfEdgeModel(
        IdentityNodeModel? fromNode = null,
        MemberNodeModel? toNode = null,
        string? fromNodeID = null,
        string? toNodeID = null,
        string? edgeID = null
    )
    {
        var fromNodeValue = fromNode ?? MockIdentityNodeModel();
        var toNodeValue = toNode ?? MockMemberNodeModel();

        return new(
            edgeID ?? $"edgeID-{Guid.NewGuid()}",
            fromNodeID ?? $"fromNodeID-{fromNodeValue.ID}",
            toNodeID ?? $"toNodeID-{toNodeValue.Id}",
            fromNodeValue,
            toNodeValue
        );
    }
}