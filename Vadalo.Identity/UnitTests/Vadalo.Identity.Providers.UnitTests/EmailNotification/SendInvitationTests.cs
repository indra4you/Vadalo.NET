using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Vadalo.Identity.Providers.EmailNotification;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class SendInvitationTests
{
    private static (
        EmailNotificationProvider emailNotificationProvider,
        Mock<Notification.IEmailNotificationService> mockEmailNotificationService
    ) Arrange(
    )
    {
        var mockEmailNotificationService = new Mock<Notification.IEmailNotificationService>(MockBehavior.Strict);
        mockEmailNotificationService
            .Setup(
                expression => expression.SendNotification(It.IsAny<Notification.EmailNotificationRequest>())
            )
            .Returns(
                Task.CompletedTask
            )
            .Verifiable();
        var emailNotificationProvider = new EmailNotificationProvider(
            mockEmailNotificationService.Object
        );

        return (emailNotificationProvider, mockEmailNotificationService);
    }

    [TestMethod]
    public async Task EmailNotificationProvider_SendInvitation_HappyPath_ShouldBeSuccessful(
    )
    {
        // Arrange
        var (emailNotificationProvider, mockEmailNotificationService) = Arrange();
        var mockInviteNotificationModel = new InviteNotificationModel(
            "invitedby@domain.com",
            "invitee@domain.com"
        );

        // Actions
        await emailNotificationProvider
            .SendInvitation(
                mockInviteNotificationModel
            );

        // Assertions
        Assert.IsNotNull(mockEmailNotificationService.Invocations);
        Assert.AreEqual(1, mockEmailNotificationService.Invocations.Count);
        Assert.AreEqual(1, mockEmailNotificationService.Invocations[0].Arguments.Count);

        Assert.IsInstanceOfType<Notification.EmailNotificationRequest>(mockEmailNotificationService.Invocations[0].Arguments[0]);

        var emailNotificationRequest = mockEmailNotificationService.Invocations[0].Arguments[0] as Notification.EmailNotificationRequest;
        Assert.IsNotNull(emailNotificationRequest);
        Assert.AreEqual(mockInviteNotificationModel.EmailAddress, emailNotificationRequest.SendTo.First());
        Assert.AreEqual("Welcome to Vadalo", emailNotificationRequest.Subject);
        Assert.IsNotNull(emailNotificationRequest.FromAssembly);
        Assert.AreEqual("Invitation", emailNotificationRequest.TemplateNameWithoutExtension);
        Assert.IsNotNull(emailNotificationRequest.KeyValues);
        Assert.IsNotNull(emailNotificationRequest.KeyValues.Keys.First());
        Assert.AreEqual("InvitedByDisplayName", emailNotificationRequest.KeyValues.Keys.First());
        Assert.IsNotNull(emailNotificationRequest.KeyValues.Values.First());
        Assert.AreEqual(mockInviteNotificationModel.InvitedByDisplayName, emailNotificationRequest.KeyValues.Values.First());
        Assert.AreEqual("Templates", emailNotificationRequest.TemplatePath);
    }
}