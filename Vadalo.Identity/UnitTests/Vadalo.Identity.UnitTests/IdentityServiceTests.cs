using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vadalo.Identity;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class IdentityServiceTests : IIdentityServiceTests
{
    private const string VALIDATION_EXCEPTION_MESSAGE = "'InviteByEmailAddressRequest' model validation failed, check validation messages for more details";

    /*  
        [TestMethod]
        public async Task IdentityService_InviteByEmailAddress_NewInvitee_SuccessfullySendInvitationToNewInvitee(
        )
        {
            // Arrange
            var identityOfEdgeModel = new Providers.IdentityOfEdgeModel(
                "edgeID",
                "fromNodeID",
                "toNodeID",
                new(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "test@domain.com"
                ),
                new(
                    Guid.NewGuid(),
                    1,
                    "test@domain.com",
                    "Last",
                    "First",
                    null
                )
            );
            var mockIdentityDataProvider = this
                .ArrangeIdentityDataProvider(
                    identityOfEdgeModel,
                    null
                );
            var mockIEmailNotificationProvider = this.ArrangeEmailNotificationProvider();

            var identityService = new IdentityService(
                mockIdentityDataProvider.Object,
                mockIEmailNotificationProvider.Object
            );
            var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
                "test@domain.com",
                "dummy@domain.com"
            );

            // Actions
            await identityService
                .InviteByEmailAddress(
                    inviteByEmailAddressRequest
                );

            // Assertions
            Assert.AreEqual(2, mockIdentityDataProvider.Invocations.Count);

            var fetchIdentityBySignInIDInvocation = mockIdentityDataProvider.Invocations[0];
            Assert.AreEqual(1, fetchIdentityBySignInIDInvocation.Arguments.Count);
            Assert.IsInstanceOfType<string>(fetchIdentityBySignInIDInvocation.Arguments[0]);
            Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, fetchIdentityBySignInIDInvocation.Arguments[0]);

            var createIdentityInvocation = mockIdentityDataProvider.Invocations[1];
            Assert.AreEqual(2, createIdentityInvocation.Arguments.Count);
            Assert.IsInstanceOfType<Guid>(createIdentityInvocation.Arguments[0]);
            Assert.AreEqual(inviteByEmailAddressRequest.InvitedByEmailAddress, createIdentityInvocation.Arguments[0]);
            Assert.IsInstanceOfType<string>(createIdentityInvocation.Arguments[1]);
            Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, createIdentityInvocation.Arguments[1]);

            Assert.AreEqual(1, mockIEmailNotificationProvider.Invocations.Count);
            var sendInvitationInvocation = mockIEmailNotificationProvider.Invocations[0];
            Assert.AreEqual(1, sendInvitationInvocation.Arguments.Count);
            Assert.IsInstanceOfType<Providers.InviteNotificationModel>(sendInvitationInvocation.Arguments[0]);
            var inviteNotificationModel = sendInvitationInvocation.Arguments[0] as Providers.InviteNotificationModel;
            Assert.IsNotNull(inviteNotificationModel);
            Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, inviteNotificationModel.EmailAddress);
            Assert.AreEqual("// TODO: Invitee Display Name", inviteNotificationModel.InvitedByDisplayName);
        }

        [TestMethod]
        public async Task IdentityService_InviteByEmailAddress_ExistingInvitee_SuccessfullySendInvitationToNewInvitee(
        )
        {
            // Arrange
            var mockIdentityNodeModel = new Providers.IdentityNodeModel(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "test@domain.com"
            );
            var mockMemberNodeModel = new Providers.MemberNodeModel(
                Guid.NewGuid(),
                0,
                "test@domain.com",
                "User",
                "Test",
                null
            );
            var mockIdentityOfEdgeModel = new Providers.IdentityOfEdgeModel(
                "edgeId",
                "fromId",
                "toId",
                mockIdentityNodeModel,
                mockMemberNodeModel
            );
            var mockIdentityDataProvider = this
                .ArrangeIdentityDataProvider(
                    mockIdentityOfEdgeModel
                );
            var mockIEmailNotificationProvider = this.ArrangeEmailNotificationProvider();

            var identityService = new IdentityService(
                mockIdentityDataProvider.Object,
                mockIEmailNotificationProvider.Object
            );
            var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
                "test@domain.com",
                "dummy@domain.com"
            );

            // Actions
            await identityService
                .InviteByEmailAddress(
                    inviteByEmailAddressRequest
                );

            // Assertions
            Assert.AreEqual(1, mockIdentityDataProvider.Invocations.Count);

            var fetchIdentityBySignInIDInvocation = mockIdentityDataProvider.Invocations[0];
            Assert.AreEqual(1, fetchIdentityBySignInIDInvocation.Arguments.Count);
            Assert.IsInstanceOfType<string>(fetchIdentityBySignInIDInvocation.Arguments[0]);
            Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, fetchIdentityBySignInIDInvocation.Arguments[0]);

            Assert.AreEqual(1, mockIEmailNotificationProvider.Invocations.Count);
            var sendInvitationInvocation = mockIEmailNotificationProvider.Invocations[0];
            Assert.AreEqual(1, sendInvitationInvocation.Arguments.Count);
            Assert.IsInstanceOfType<Providers.InviteNotificationModel>(sendInvitationInvocation.Arguments[0]);
            var inviteNotificationModel = sendInvitationInvocation.Arguments[0] as Providers.InviteNotificationModel;
            Assert.IsNotNull(inviteNotificationModel);
            Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, inviteNotificationModel.EmailAddress);
            Assert.AreEqual("// TODO: Invitee Display Name", inviteNotificationModel.InvitedByDisplayName);
        }

        [TestMethod]
        public async Task IdentityService_InviteByEmailAddress_NullRequestModel_ThrowsValidationException(
        )
        {
            // Arrange
            var mockIdentityDataProvider = this
                .ArrangeIdentityDataProvider(
                    null
                );
            var mockIEmailNotificationProvider = this.ArrangeEmailNotificationProvider();

            var identityService = new IdentityService(
                mockIdentityDataProvider.Object,
                mockIEmailNotificationProvider.Object
            );

            // Actions
            async Task action() => await identityService
                .InviteByEmailAddress(
    #pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    null
    #pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                );

            // Assertions
            var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
            Assert.AreEqual(1, validationException.ValidationMessages.Count());

            var requestModelValidationMessage = validationException.ValidationMessages.First();
            Assert.IsInstanceOfType<string>(requestModelValidationMessage);
            Assert.AreEqual($"inviteByEmailAddressRequest:null", requestModelValidationMessage);
        }

        [TestMethod]
        public async Task IdentityService_InviteByEmailAddress_EmptyInvitedBy_ThrowsValidationException(
        )
        {
            // Arrange
            var mockIdentityDataProvider = this
                .ArrangeIdentityDataProvider(
                    null
                );
            var mockIEmailNotificationProvider = this.ArrangeEmailNotificationProvider();

            var identityService = new IdentityService(
                mockIdentityDataProvider.Object,
                mockIEmailNotificationProvider.Object
            );
            var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
                "test@domain.com",
                "dummy@domain.com"
            );

            // Actions
            async Task action() => await identityService
                .InviteByEmailAddress(
                    inviteByEmailAddressRequest
                );

            // Assertions
            var identityOfEdgeNotFoundException = await Assert.ThrowsExceptionAsync<IdentityOfEdgeNotFoundException>(action);
            Assert.AreEqual($"Invitee with Email Address '{inviteByEmailAddressRequest.InviteeEmailAddress}' not found", identityOfEdgeNotFoundException.Message);
        }

        [DataTestMethod]
        [DataRow(null, "null")]
        [DataRow("", "empty")]
        [DataRow(" ", "whitespaces")]
        [DataRow("a", "not-email")]
        public async Task IdentityService_InviteByEmailAddress_InvalidSignInID_ThrowsValidationException(
            string signInIDValue,
            string expectedExceptionMessageSuffix
        )
        {
            // Arrange
            var mockIdentityDataProvider = this
                .ArrangeIdentityDataProvider(
                    null
                );
            var mockIEmailNotificationProvider = this.ArrangeEmailNotificationProvider();

            var identityService = new IdentityService(
                mockIdentityDataProvider.Object,
                mockIEmailNotificationProvider.Object
            );
            var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
                "test@domain.com",
                signInIDValue
            );

            // Actions
            async Task action() => await identityService
                .InviteByEmailAddress(
                    inviteByEmailAddressRequest
                );

            // Assertions
            var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
            Assert.AreEqual(1, validationException.ValidationMessages.Count());

            var signInIDValidationMessage = validationException.ValidationMessages.First();
            Assert.IsInstanceOfType<string>(signInIDValidationMessage);
            Assert.AreEqual($"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InviteeEmailAddress)}:{expectedExceptionMessageSuffix}", signInIDValidationMessage);
        }

        [TestMethod]
        public async Task IdentityService_InviteByEmailAddress_EmptyInvitedByAndSignInID_ThrowsValidationException(
        )
        {
            // Arrange
            var mockIdentityDataProvider = this
                .ArrangeIdentityDataProvider(
                    null
                );
            var mockIEmailNotificationProvider = this.ArrangeEmailNotificationProvider();

            var identityService = new IdentityService(
                mockIdentityDataProvider.Object,
                mockIEmailNotificationProvider.Object
            );
            var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
                string.Empty,
                string.Empty
            );

            // Actions
            async Task action() => await identityService
                .InviteByEmailAddress(
                    inviteByEmailAddressRequest
                );

            // Assertions
            var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
            Assert.AreEqual(2, validationException.ValidationMessages.Count());

            var invitedByValidationMessage = validationException.ValidationMessages.First();
            Assert.IsInstanceOfType<string>(invitedByValidationMessage);
            Assert.AreEqual($"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InvitedByEmailAddress)}:empty", invitedByValidationMessage);

            var signInIDValidationMessage = validationException.ValidationMessages.Last();
            Assert.IsInstanceOfType<string>(signInIDValidationMessage);
            Assert.AreEqual($"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InviteeEmailAddress)}:empty", signInIDValidationMessage);
        }
    */

    [TestMethod]
    public async Task IdentityService_InviteByEmailAddress_NullRequestModel_ThrowsValidationException(
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockIEmailNotificationProvider.Object
        );

        // Actions
        async Task action() => await identityService
            .InviteByEmailAddress(
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
        Assert.AreEqual("inviteByEmailAddressRequest:null", requestModelValidationMessage);
    }

    [TestMethod]
    public async Task IdentityService_InviteByEmailAddress_NullRequestValues_ThrowsValidationException(
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            null,
            null
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        );

        // Actions
        async Task action() => await identityService
            .InviteByEmailAddress(
                inviteByEmailAddressRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.IsNotNull(validationException);
        Assert.AreEqual(VALIDATION_EXCEPTION_MESSAGE, validationException.Message);
        Assert.AreEqual(2, validationException.ValidationMessages.Count());

        var validationMessageEnumerator = validationException.ValidationMessages.GetEnumerator();
        Assert.IsNotNull(validationMessageEnumerator);

        validationMessageEnumerator.MoveNext();
        Assert.IsInstanceOfType<string>(validationMessageEnumerator.Current);
        Assert.AreEqual($"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InvitedByEmailAddress)}:null", validationMessageEnumerator.Current);

        validationMessageEnumerator.MoveNext();
        Assert.IsInstanceOfType<string>(validationMessageEnumerator.Current);
        Assert.AreEqual($"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InviteeEmailAddress)}:null", validationMessageEnumerator.Current);
    }

    [DataTestMethod]
    [DataRow(null, "null")]
    [DataRow("", "empty")]
    [DataRow(" ", "whitespaces")]
    [DataRow("a", "not-email")]
    public async Task IdentityService_InviteByEmailAddress_InvalidInvitedByEmailAddress_ThrowsValidationException(
            string invitedByEmailAddress,
            string expectedExceptionMessageSuffix
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
            invitedByEmailAddress,
            Extensions.INVITEE_EMAIL_ADDRESS
        );

        // Actions
        async Task action() => await identityService
            .InviteByEmailAddress(
                inviteByEmailAddressRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.IsNotNull(validationException);
        Assert.AreEqual(VALIDATION_EXCEPTION_MESSAGE, validationException.Message);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var invitedByEmailAddressValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(invitedByEmailAddressValidationMessage);
        Assert.AreEqual($"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InvitedByEmailAddress)}:{expectedExceptionMessageSuffix}", invitedByEmailAddressValidationMessage);
    }

    [DataTestMethod]
    [DataRow(null, "null")]
    [DataRow("", "empty")]
    [DataRow(" ", "whitespaces")]
    [DataRow("a", "not-email")]
    public async Task IdentityService_InviteByEmailAddress_InvalidInviteeEmailAddress_ThrowsValidationException(
            string inviteeEmailAddress,
            string expectedExceptionMessageSuffix
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
            Extensions.INVITED_BY_EMAIL_ADDRESS,
            inviteeEmailAddress
        );

        // Actions
        async Task action() => await identityService
            .InviteByEmailAddress(
                inviteByEmailAddressRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.IsNotNull(validationException);
        Assert.AreEqual(VALIDATION_EXCEPTION_MESSAGE, validationException.Message);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var inviteeEmailAddressValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(inviteeEmailAddressValidationMessage);
        Assert.AreEqual($"{nameof(inviteByEmailAddressRequest)}.{nameof(inviteByEmailAddressRequest.InviteeEmailAddress)}:{expectedExceptionMessageSuffix}", inviteeEmailAddressValidationMessage);
    }

    [TestMethod]
    public async Task IdentityService_InviteByEmailAddress_InvitedByNotFound_ThrowsNotFoundException(
    )
    {
        // Arrange
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider();
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
            Extensions.INVITED_BY_EMAIL_ADDRESS,
            Extensions.INVITEE_EMAIL_ADDRESS
        );

        // Actions
        async Task action() => await identityService
            .InviteByEmailAddress(
                inviteByEmailAddressRequest
            );

        // Assertions
        var identityOfEdgeNotFoundException = await Assert.ThrowsExceptionAsync<IdentityOfEdgeNotFoundException>(action);
        Assert.AreEqual($"Invitee with Email Address '{inviteByEmailAddressRequest.InviteeEmailAddress}' not found", identityOfEdgeNotFoundException.Message);
    }

    [TestMethod]
    public async Task IdentityService_InviteByEmailAddress_InviteeNotFound_SuccessfullyCreateIdentityAndSendInvitationToNewInvitee(
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
        var mockIdentityDataProvider = this
            .ArrangeIdentityDataProvider(
                mockIdentityOfEdgeModel
            );
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
            mockIdentityOfEdgeModel.FromNode.SignInID,
            Extensions.INVITEE_EMAIL_ADDRESS
        );

        // Actions
        await identityService
            .InviteByEmailAddress(
                inviteByEmailAddressRequest
            );

        // Assertions
        Assert.AreEqual(3, mockIdentityDataProvider.Invocations.Count);

        var mockIdentityDataProviderInvocationEnumerator = mockIdentityDataProvider.Invocations.GetEnumerator();
        Assert.IsNotNull(mockIdentityDataProviderInvocationEnumerator);

        mockIdentityDataProviderInvocationEnumerator.MoveNext();
        Assert.AreEqual(1, mockIdentityDataProviderInvocationEnumerator.Current.Arguments.Count);
        Assert.IsInstanceOfType<string>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.AreEqual(inviteByEmailAddressRequest.InvitedByEmailAddress, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);

        mockIdentityDataProviderInvocationEnumerator.MoveNext();
        Assert.AreEqual(1, mockIdentityDataProviderInvocationEnumerator.Current.Arguments.Count);
        Assert.IsInstanceOfType<string>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);

        mockIdentityDataProviderInvocationEnumerator.MoveNext();
        Assert.AreEqual(2, mockIdentityDataProviderInvocationEnumerator.Current.Arguments.Count);
        Assert.IsInstanceOfType<Guid>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.AreEqual(mockIdentityOfEdgeModel.FromNode.ID, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.IsInstanceOfType<string>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[1]);
        Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[1]);

        Assert.AreEqual(1, mockIEmailNotificationProvider.Invocations.Count);
        var sendInvitationInvocation = mockIEmailNotificationProvider.Invocations[0];
        Assert.AreEqual(1, sendInvitationInvocation.Arguments.Count);
        Assert.IsInstanceOfType<Providers.InviteNotificationModel>(sendInvitationInvocation.Arguments[0]);
        var inviteNotificationModel = sendInvitationInvocation.Arguments[0] as Providers.InviteNotificationModel;
        Assert.IsNotNull(inviteNotificationModel);
        Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, inviteNotificationModel.EmailAddress);
        Assert.AreEqual(mockIdentityOfEdgeModel.ToNode.ToDisplayName(), inviteNotificationModel.InvitedByDisplayName);
    }

    [TestMethod]
    public async Task IdentityService_InviteByEmailAddress_ExistingInvitee_SuccessfullySendInvitationToInvitee(
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
        var mockIEmailNotificationProvider = this
            .ArrangeEmailNotificationProvider();

        var identityService = new IdentityService(
            mockIdentityDataProvider.Object,
            mockIEmailNotificationProvider.Object
        );
        var inviteByEmailAddressRequest = new InviteByEmailAddressRequest(
            mockIdentityOfEdgeModel.FromNode.SignInID,
            Extensions.INVITEE_EMAIL_ADDRESS
        );

        // Actions
        await identityService
            .InviteByEmailAddress(
                inviteByEmailAddressRequest
            );

        // Assertions
        Assert.AreEqual(2, mockIdentityDataProvider.Invocations.Count);

        var mockIdentityDataProviderInvocationEnumerator = mockIdentityDataProvider.Invocations.GetEnumerator();
        Assert.IsNotNull(mockIdentityDataProviderInvocationEnumerator);

        mockIdentityDataProviderInvocationEnumerator.MoveNext();
        Assert.AreEqual(1, mockIdentityDataProviderInvocationEnumerator.Current.Arguments.Count);
        Assert.IsInstanceOfType<string>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.AreEqual(inviteByEmailAddressRequest.InvitedByEmailAddress, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);

        mockIdentityDataProviderInvocationEnumerator.MoveNext();
        Assert.AreEqual(1, mockIdentityDataProviderInvocationEnumerator.Current.Arguments.Count);
        Assert.IsInstanceOfType<string>(mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);
        Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, mockIdentityDataProviderInvocationEnumerator.Current.Arguments[0]);

        Assert.AreEqual(1, mockIEmailNotificationProvider.Invocations.Count);
        var sendInvitationInvocation = mockIEmailNotificationProvider.Invocations[0];
        Assert.AreEqual(1, sendInvitationInvocation.Arguments.Count);
        Assert.IsInstanceOfType<Providers.InviteNotificationModel>(sendInvitationInvocation.Arguments[0]);
        var inviteNotificationModel = sendInvitationInvocation.Arguments[0] as Providers.InviteNotificationModel;
        Assert.IsNotNull(inviteNotificationModel);
        Assert.AreEqual(inviteByEmailAddressRequest.InviteeEmailAddress, inviteNotificationModel.EmailAddress);
        Assert.AreEqual(mockIdentityOfEdgeModel.ToNode.ToDisplayName(), inviteNotificationModel.InvitedByDisplayName);
    }
}