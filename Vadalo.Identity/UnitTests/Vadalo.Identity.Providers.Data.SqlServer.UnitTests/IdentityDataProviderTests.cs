using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Vadalo.Identity.Providers;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class IdentityDataProviderTests : Data.IDataProviderTests
{
    [TestMethod]
    public async Task IdentityDataProvider_CreateIdentity_HappyPath_ShouldBeSuccessful(
    )
    {
        // Arrange
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteNonQuery(1);
        var identityDataProvider = new IdentityDataProvider(mockDataProvider.Object);
        var invitedBy = Guid.NewGuid();
        var signInID = "test@abc.com";

        // Actions
        await identityDataProvider
            .CreateIdentity(
                invitedBy,
                signInID
            );

        // Assertions
        Assert.IsNotNull(mockDataProvider.Invocations);
        Assert.AreEqual(1, mockDataProvider.Invocations.Count);
        Assert.AreEqual(2, mockDataProvider.Invocations[0].Arguments.Count);

        Assert.IsInstanceOfType<string>(mockDataProvider.Invocations[0].Arguments[0]);
        Assert.IsInstanceOfType<IEnumerable<Data.ParameterModel>>(mockDataProvider.Invocations[0].Arguments[1]);

        var parameters = mockDataProvider.Invocations[0].Arguments[1] as IEnumerable<Data.ParameterModel>;
        Assert.IsNotNull(parameters);

        var parameterEnumerator = parameters.GetEnumerator();
        parameterEnumerator.MoveNext();
        Assert.AreEqual("@_invited_by", parameterEnumerator.Current.Name);
        Assert.AreEqual(invitedBy, parameterEnumerator.Current.Value);
        Assert.AreEqual(DbType.Guid, parameterEnumerator.Current.DbType);

        parameterEnumerator.MoveNext();
        Assert.AreEqual("@_sign_in_id", parameterEnumerator.Current.Name);
        Assert.AreEqual(signInID, parameterEnumerator.Current.Value);
        Assert.IsNull(parameterEnumerator.Current.DbType);
    }

    [TestMethod]
    public async Task IdentityDataProvider_CreateIdentity_DatabaseException_ShouldThrowException(
    )
    {
        // Arrange
        var exception = new Exception();
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteNonQuery(
                exception
            );
        var identityDataProvider = new IdentityDataProvider(mockDataProvider.Object);
        var invitedBy = Guid.NewGuid();
        var signInID = "test@abc.com";

        // Actions
        async Task action() => await identityDataProvider
            .CreateIdentity(
                invitedBy,
                signInID
            );

        // Assertions
        await Assert.ThrowsExceptionAsync<Exception>(action);
    }
}