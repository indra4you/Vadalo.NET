using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Vadalo.Identity.Providers;

public sealed class IdentityDataProvider(
    Data.IDataProvider dataProvider
) : IIdentityDataProvider
{
    private readonly Data.IDataProvider _dataProvider = dataProvider;

    public async Task<IdentityOfEdgeModel?> FetchIdentityOfEdgeByEmailAddress(
        string emailAddress
    )
    {
        var parameters = _dataProvider.CreateParameters()
            .AddParameter("@_email_address", emailAddress);

        var identityOfEdge = await _dataProvider
            .ExecuteReader(
                @"
                    SELECT
	                      [identities].[id] AS identity_id
						, [identities].[invited_by] AS identity_invited_by
						, [identities].[sign_in_id] AS identity_sign_in_id
	                    , [identities_of].$edge_id AS identity_of_edge_id
						, [identities_of].$from_id AS identity_of_from_id
						, [identities_of].$to_id AS identity_of_to_id
	                    , [members].[id] AS member_id
						, [members].[status] AS member_status
						, [members].[email_address] AS member_email_address
						, [members].[last_name] AS member_last_name
						, [members].[first_name] AS member_first_name
						, [members].[middle_name] AS member_middle_name
                    FROM
	                      [dbo].[identities]
	                    , [dbo].[identities_of]
	                    , [dbo].[members]
                    WHERE
	                    MATCH ( [identities] - ( [identities_of] ) -> [members] )
                    AND [members].[email_address] = @_email_address
                ",
                parameters,
                MapperExtensions.ToIdentityOfEdgeModel
            );

        return identityOfEdge
            .FirstOrDefault();
    }

    public async Task<IdentityNodeModel?> FetchIdentityNodeBySignInID(
        string signInID
    )
    {
        var parameters = _dataProvider.CreateParameters()
            .AddParameter("@_sign_in_id", signInID);

        var identityNodes = await _dataProvider
            .ExecuteReader(
                @"
                    SELECT
	                      [identities].[id] AS identity_id
						, [identities].[invited_by] AS identity_invited_by
						, [identities].[sign_in_id] AS identity_sign_in_id
                    FROM
	                      [dbo].[identities]
                    WHERE
                        [identities].[sign_in_id] = @_sign_in_id
                ",
                parameters,
                MapperExtensions.ToIdentityNodeModel
            );

        return identityNodes
            .FirstOrDefault();
    }

    public async Task CreateIdentity(
        Guid invitedBy,
        string signInID
    )
    {
        var parameters = _dataProvider.CreateParameters()
            .AddParameter("@_invited_by", invitedBy, DbType.Guid)
            .AddParameter("@_sign_in_id", signInID);

        await _dataProvider
            .ExecuteNonQuery(
                @"
                    INSERT INTO [dbo].[identities]
                    (
                          [invited_by]
                        , [sign_in_id]
                    )
                    VALUES (
                          @_invited_by
                        , @_sign_in_id
                    )
                ",
                parameters
            );
    }
}