using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Vadalo.Identity.Providers.IdentityData;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class FetchIdentityOfEdgeByEmailAddressTests : Data.IDataProviderTests
{
    [TestMethod]
    public async Task IdentityDataProvider_FetchIdentityOfEdgeByEmailAddress_HappyPath_ShouldReturnValidModel(
    )
    {
        // Arrange
        var mockIdentityOfEdgeModel = Extensions
            .MockIdentityOfEdgeModel();
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteReader(
                [mockIdentityOfEdgeModel]
            );
        var identityDataProvider = new IdentityDataProvider(
            mockDataProvider.Object
        );
        var emailAddress = "test@abc.com";

        // Actions
        var identityOfEdgeModel = await identityDataProvider
            .FetchIdentityOfEdgeByEmailAddress(
                emailAddress
            );

        // Assertions
        Assert.IsNotNull(identityOfEdgeModel);
        Assert.AreEqual(mockIdentityOfEdgeModel, identityOfEdgeModel);

        Assert.IsNotNull(mockDataProvider.Invocations);
        Assert.AreEqual(1, mockDataProvider.Invocations.Count);
        Assert.AreEqual(3, mockDataProvider.Invocations[0].Arguments.Count);

        Assert.IsInstanceOfType<string>(mockDataProvider.Invocations[0].Arguments[0]);
        Assert.IsInstanceOfType<IEnumerable<Data.ParameterModel>>(mockDataProvider.Invocations[0].Arguments[1]);
        Assert.IsInstanceOfType<Func<DbDataReader, IdentityOfEdgeModel>>(mockDataProvider.Invocations[0].Arguments[2]);

        var parameters = mockDataProvider.Invocations[0].Arguments[1] as IEnumerable<Data.ParameterModel>;
        Assert.IsNotNull(parameters);

        var parameterEnumerator = parameters.GetEnumerator();
        parameterEnumerator.MoveNext();
        Assert.AreEqual("@_email_address", parameterEnumerator.Current.Name);
        Assert.AreEqual(emailAddress, parameterEnumerator.Current.Value);
        Assert.IsNull(parameterEnumerator.Current.DbType);

        var dataReaderFunction = mockDataProvider.Invocations[0].Arguments[2] as Func<DbDataReader, IdentityOfEdgeModel>;
        Assert.IsNotNull(dataReaderFunction);
    }

    [TestMethod]
    public async Task IdentityDataProvider_FetchIdentityOfEdgeByEmailAddress_IdentityEdgeNotFound_ShouldReturnNull(
    )
    {
        // Arrange
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteReader(
                Array.Empty<IdentityOfEdgeModel>()
            );
        var identityDataProvider = new IdentityDataProvider(
            mockDataProvider.Object
        );
        var emailAddress = "test@abc.com";

        // Actions
        var identityOfEdgeModel = await identityDataProvider
            .FetchIdentityOfEdgeByEmailAddress(
                emailAddress
            );

        // Assertions
        Assert.IsNull(identityOfEdgeModel);

        Assert.IsNotNull(mockDataProvider.Invocations);
        Assert.AreEqual(1, mockDataProvider.Invocations.Count);
        Assert.AreEqual(3, mockDataProvider.Invocations[0].Arguments.Count);

        Assert.IsInstanceOfType<string>(mockDataProvider.Invocations[0].Arguments[0]);
        Assert.IsInstanceOfType<IEnumerable<Data.ParameterModel>>(mockDataProvider.Invocations[0].Arguments[1]);
        Assert.IsInstanceOfType<Func<DbDataReader, IdentityOfEdgeModel>>(mockDataProvider.Invocations[0].Arguments[2]);

        var parameters = mockDataProvider.Invocations[0].Arguments[1] as IEnumerable<Data.ParameterModel>;
        Assert.IsNotNull(parameters);

        var parameterEnumerator = parameters.GetEnumerator();
        parameterEnumerator.MoveNext();
        Assert.AreEqual("@_email_address", parameterEnumerator.Current.Name);
        Assert.AreEqual(emailAddress, parameterEnumerator.Current.Value);
        Assert.IsNull(parameterEnumerator.Current.DbType);

        var dataReaderFunction = mockDataProvider.Invocations[0].Arguments[2] as Func<DbDataReader, IdentityOfEdgeModel>;
        Assert.IsNotNull(dataReaderFunction);
    }

    [TestMethod]
    public async Task IdentityDataProvider_FetchIdentityOfEdgeByEmailAddress_DatabaseException_ShouldThrowException(
    )
    {
        // Arrange
        var exception = new Exception();
        var mockDataProvider = this.ArrangeDataProvider()
            .MockExecuteReader<IdentityOfEdgeModel>(
                exception
            );
        var identityDataProvider = new IdentityDataProvider(
            mockDataProvider.Object
        );
        var emailAddress = "test@abc.com";

        // Actions
        async Task action() => await identityDataProvider
            .FetchIdentityOfEdgeByEmailAddress(
                emailAddress
            );

        // Assertions
        await Assert.ThrowsExceptionAsync<Exception>(action);
    }
}