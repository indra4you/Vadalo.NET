using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vadalo.Identity.Service;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class SendOneTimePasswordByEmailAddressTests : IIdentityServiceTests
{
    private const string VALIDATION_EXCEPTION_MESSAGE = "'OneTimePasswordRequest' model validation failed, check validation messages for more details";

    [TestMethod]
    public async Task IdentityService_SendOneTimePasswordByEmailAddress_NullRequestModel_ThrowsValidationException(
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockPasswordProvider = this
            .ArrangePasswordProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockPasswordProvider.Object,
            mockIEmailNotificationProvider.Object
        );

        // Actions
        async Task action() => await identityService
            .SendOneTimePasswordByEmailAddress(
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                null
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.IsNotNull(validationException);
        Assert.AreEqual(VALIDATION_EXCEPTION_MESSAGE, validationException.Message);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var requestModelValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(requestModelValidationMessage);
        Assert.AreEqual("oneTimePasswordRequest:null", requestModelValidationMessage);
    }

    [DataTestMethod]
    [DataRow(null, "null")]
    [DataRow("", "empty")]
    [DataRow(" ", "whitespaces")]
    [DataRow("a", "not-email")]
    public async Task IdentityService_SendOneTimePasswordByEmailAddress_InvalidInvitedByEmailAddress_ThrowsValidationException(
        string emailAddress,
        string expectedExceptionMessageSuffix
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockPasswordProvider = this
            .ArrangePasswordProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockPasswordProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var oneTimePasswordRequest = new OneTimePasswordRequest
        {
            EmailAddress = emailAddress
        };

        // Actions
        async Task action() => await identityService
            .SendOneTimePasswordByEmailAddress(
                oneTimePasswordRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.IsNotNull(validationException);
        Assert.AreEqual(VALIDATION_EXCEPTION_MESSAGE, validationException.Message);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var invitedByEmailAddressValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(invitedByEmailAddressValidationMessage);
        Assert.AreEqual($"{nameof(oneTimePasswordRequest)}.{nameof(oneTimePasswordRequest.EmailAddress)}:{expectedExceptionMessageSuffix}", invitedByEmailAddressValidationMessage);
    }

    [TestMethod]
    public async Task IdentityService_SendOneTimePasswordByEmailAddress_EmailAddressNotFound_ThrowsNotFoundException(
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockPasswordProvider = this
            .ArrangePasswordProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockPasswordProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var oneTimePasswordRequest = new OneTimePasswordRequest
        {
            EmailAddress = Extensions.INVITEE_EMAIL_ADDRESS
        };

        // Actions
        async Task action() => await identityService
            .SendOneTimePasswordByEmailAddress(
                oneTimePasswordRequest
            );

        // Assertions
        var identityOfEdgeNotFoundException = await Assert.ThrowsExceptionAsync<IdentityNodeNotFoundException>(action);
        Assert.AreEqual($"Identity with Email Address '{oneTimePasswordRequest.EmailAddress}' not found", identityOfEdgeNotFoundException.Message);
    }

    [TestMethod]
    public async Task IdentityService_SendOneTimePasswordByEmailAddress_EmailAddressExists_SuccessfullySendOneTimePassword(
    )
    {
        // Arrange
        var mockFromNode = this
            .MockIdentityNodeModel(
                signInID: Extensions.INVITED_BY_EMAIL_ADDRESS
            );
        var mockIdentityOfEdgeModel = this
            .MockIdentityOfEdgeModel(
                fromNode: mockFromNode
            );
        var mockIdentityNodeModel = this
            .MockIdentityNodeModel();
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider(
                mockIdentityOfEdgeModel,
                mockIdentityNodeModel
            );
        var mockPasswordProvider = this
            .ArrangePasswordProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockPasswordProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var oneTimePasswordRequest = new OneTimePasswordRequest
        {
            EmailAddress = Extensions.INVITEE_EMAIL_ADDRESS
        };

        // Actions
        await identityService
            .SendOneTimePasswordByEmailAddress(
                oneTimePasswordRequest
            );

        // Assertions
        Assert.AreEqual(2, mockIdentityDataProvider.Invocations.Count);

        var mockIdentityDataProviderInvocationEnumerator = mockIdentityDataProvider.Invocations.GetEnumerator();
        Assert.IsNotNull(mockIdentityDataProviderInvocationEnumerator);

        mockIdentityDataProviderInvocationEnumerator.MoveNext();
        Assert.AreEqual(1, mockIdentityDataProviderInvocationEnumerator.Current.Arguments.Count);
        Assert.IsInstanceOfType<string>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.AreEqual(oneTimePasswordRequest.EmailAddress, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);

        mockIdentityDataProviderInvocationEnumerator.MoveNext();
        Assert.AreEqual(2, mockIdentityDataProviderInvocationEnumerator.Current.Arguments.Count);
        Assert.IsInstanceOfType<Guid>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.AreEqual(mockIdentityNodeModel.ID, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.IsInstanceOfType<string>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[1]);

        Assert.AreEqual(1, mockPasswordProvider.Invocations.Count);

        Assert.AreEqual(1, mockIEmailNotificationProvider.Invocations.Count);
        var sendOneTimePasswordInvocation = mockIEmailNotificationProvider.Invocations[0];
        Assert.AreEqual(1, sendOneTimePasswordInvocation.Arguments.Count);
        Assert.IsInstanceOfType<Providers.OneTimePasswordNotificationModel>(sendOneTimePasswordInvocation.Arguments[0]);
        var oneTimePasswordNotificationModel = sendOneTimePasswordInvocation.Arguments[0] as Providers.OneTimePasswordNotificationModel;
        Assert.IsNotNull(oneTimePasswordNotificationModel);
        Assert.AreEqual(oneTimePasswordRequest.EmailAddress, oneTimePasswordNotificationModel.EmailAddress);
        Assert.IsNotNull(oneTimePasswordNotificationModel.OneTimePassword);
    }
}