using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Vadalo.Identity.Providers.IdentityData;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class FetchIdentityNodeBySignInIDTests : Data.IDataProviderTests
{
    [TestMethod]
    public async Task IdentityDataProvider_FetchIdentityNodeBySignInID_HappyPath_ShouldReturnValidModel(
    )
    {
        // Arrange
        var mockIdentityNodeModel = Extensions
            .MockIdentityNodeModel();
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteReader(
                [mockIdentityNodeModel]
            );
        var identityDataProvider = new IdentityDataProvider(
            mockDataProvider.Object
        );
        var signInID = "test@abc.com";

        // Actions
        var identityNodeModel = await identityDataProvider
            .FetchIdentityNodeBySignInID(
                signInID
            );

        // Assertions
        Assert.IsNotNull(identityNodeModel);
        Assert.AreEqual(mockIdentityNodeModel, identityNodeModel);

        Assert.IsNotNull(mockDataProvider.Invocations);
        Assert.AreEqual(1, mockDataProvider.Invocations.Count);
        Assert.AreEqual(3, mockDataProvider.Invocations[0].Arguments.Count);

        Assert.IsInstanceOfType<string>(mockDataProvider.Invocations[0].Arguments[0]);
        Assert.IsInstanceOfType<IEnumerable<Data.ParameterModel>>(mockDataProvider.Invocations[0].Arguments[1]);
        Assert.IsInstanceOfType<Func<DbDataReader, IdentityNodeModel>>(mockDataProvider.Invocations[0].Arguments[2]);

        var parameters = mockDataProvider.Invocations[0].Arguments[1] as IEnumerable<Data.ParameterModel>;
        Assert.IsNotNull(parameters);

        var parameterEnumerator = parameters.GetEnumerator();
        parameterEnumerator.MoveNext();
        Assert.AreEqual("@_sign_in_id", parameterEnumerator.Current.Name);
        Assert.AreEqual(signInID, parameterEnumerator.Current.Value);
        Assert.IsNull(parameterEnumerator.Current.DbType);

        var dataReaderFunction = mockDataProvider.Invocations[0].Arguments[2] as Func<DbDataReader, IdentityNodeModel>;
        Assert.IsNotNull(dataReaderFunction);
    }

    [TestMethod]
    public async Task IdentityDataProvider_FetchIdentityNodeBySignInID_IdentityEdgeNotFound_ShouldReturnNull(
    )
    {
        // Arrange
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteReader(
                Array.Empty<IdentityNodeModel>()
            );
        var identityDataProvider = new IdentityDataProvider(
            mockDataProvider.Object
        );
        var signInID = "test@abc.com";

        // Actions
        var identityNodeModel = await identityDataProvider
            .FetchIdentityNodeBySignInID(
                signInID
            );

        // Assertions
        Assert.IsNull(identityNodeModel);

        Assert.IsNotNull(mockDataProvider.Invocations);
        Assert.AreEqual(1, mockDataProvider.Invocations.Count);
        Assert.AreEqual(3, mockDataProvider.Invocations[0].Arguments.Count);

        Assert.IsInstanceOfType<string>(mockDataProvider.Invocations[0].Arguments[0]);
        Assert.IsInstanceOfType<IEnumerable<Data.ParameterModel>>(mockDataProvider.Invocations[0].Arguments[1]);
        Assert.IsInstanceOfType<Func<DbDataReader, IdentityNodeModel>>(mockDataProvider.Invocations[0].Arguments[2]);

        var parameters = mockDataProvider.Invocations[0].Arguments[1] as IEnumerable<Data.ParameterModel>;
        Assert.IsNotNull(parameters);

        var parameterEnumerator = parameters.GetEnumerator();
        parameterEnumerator.MoveNext();
        Assert.AreEqual("@_sign_in_id", parameterEnumerator.Current.Name);
        Assert.AreEqual(signInID, parameterEnumerator.Current.Value);
        Assert.IsNull(parameterEnumerator.Current.DbType);

        var dataReaderFunction = mockDataProvider.Invocations[0].Arguments[2] as Func<DbDataReader, IdentityNodeModel>;
        Assert.IsNotNull(dataReaderFunction);
    }

    [TestMethod]
    public async Task IdentityDataProvider_FetchIdentityNodeBySignInID_DatabaseException_ShouldThrowException(
    )
    {
        // Arrange
        var exception = new Exception();
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteReader<IdentityNodeModel>(
                exception
            );
        var identityDataProvider = new IdentityDataProvider(
            mockDataProvider.Object
        );
        var signInID = "test@abc.com";

        // Actions
        async Task action() => await identityDataProvider
            .FetchIdentityNodeBySignInID(
                signInID
            );

        // Assertions
        await Assert.ThrowsExceptionAsync<Exception>(action);
    }
}