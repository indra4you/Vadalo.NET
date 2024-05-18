using System.Data.Common;

namespace Vadalo.Identity.Providers;

internal static class MapperExtensions
{
    internal static IdentityNodeModel ToIdentityNodeModel(
        DbDataReader dataReader
    ) =>
        new(
            dataReader
                .GetGuid(
                    "identity_id"
                ),
            dataReader
                .GetGuid(
                    "identity_invited_by"
                ),
            dataReader
                .GetString(
                    "identity_sign_in_id"
                )
        );

    internal static MemberNodeModel ToMemberNodeModel(
        DbDataReader dataReader
    ) =>
        new(
            dataReader
                .GetGuid(
                    "member_id"
                ),
            dataReader
                .GetByte(
                    "member_status"
                ),
            dataReader
                .GetString(
                    "member_email_address"
                ),
            dataReader
                .GetString(
                    "member_last_name"
                ),
            dataReader
                .GetString(
                    "member_first_name"
                ),
            dataReader
                .GetNullableString(
                    "member_middle_name"
                )
        );

    internal static IdentityOfEdgeModel ToIdentityOfEdgeModel(
        DbDataReader dataReader
    ) =>
        new(
            dataReader
                .GetString(
                    "identity_of_edge_id"
                ),
            dataReader
                .GetString(
                    "identity_of_from_id"
                ),
            dataReader
                .GetString(
                    "identity_of_to_id"
                ),
            MapperExtensions.ToIdentityNodeModel(
                dataReader
            ),
            MapperExtensions.ToMemberNodeModel(
                dataReader
            )
        );

    internal static PassHashNodeModel ToPassHashNodeModel(
        DbDataReader dataReader
    ) =>
        new(
            dataReader
                .GetGuid(
                    "pass_hashes_identity_id"
                ),
            dataReader
                .GetString(
                    "pass_hashes_pass_hash"
                )
        );
}