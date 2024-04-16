using Vadalo.Identity.Providers;

namespace Vadalo.Identity;

internal static class Extensions
{
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
}