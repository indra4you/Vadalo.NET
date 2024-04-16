using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Vadalo.Notification;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class EmailNotificationToServerServiceUnitTests
{
    private static EmailNotificationToServerService Arrange(
    )
    {
        var emailNotificationOptions = new EmailNotificationToServerOptions()
        {
            ServerRootUrl = "http://localhost/",
            FromEmailAddress = "support@vadalo.in",
            SmtpHost = "http://smpt-host/",
            SmtpPort = 25,
            EnableSSL = false,
            UserName = "user-name",
            Password = "password"
        };

        var emailNotificationService = new EmailNotificationToServerService(
            emailNotificationOptions
        );

        return emailNotificationService;
    }

    [TestMethod]
    public async Task EmailNotificationToServerService_SendNotification_NullRequest_ThrowsValidationException(
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

        // Actions
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        async Task action() => await emailNotificationService
            .SendNotification(
                null
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var requestModelValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(requestModelValidationMessage);
        Assert.AreEqual($"emailNotificationRequest:null", requestModelValidationMessage);
    }

    [TestMethod]
    public async Task EmailNotificationToServerService_SendNotification_NullAllRequestValues_ThrowsValidationException(
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var emailNotificationRequest = new EmailNotificationRequest(
            null,
            null,
            null,
            null
        );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // Actions
        async Task action() => await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.AreEqual(4, validationException.ValidationMessages.Count());

        var requestModelValidationMessageEnumerator = validationException.ValidationMessages.GetEnumerator();

        requestModelValidationMessageEnumerator.MoveNext();
        Assert.IsInstanceOfType<string>(requestModelValidationMessageEnumerator.Current);
        Assert.AreEqual("emailNotificationRequest.SendTo:null", requestModelValidationMessageEnumerator.Current);

        requestModelValidationMessageEnumerator.MoveNext();
        Assert.IsInstanceOfType<string>(requestModelValidationMessageEnumerator.Current);
        Assert.AreEqual("emailNotificationRequest.Subject:null", requestModelValidationMessageEnumerator.Current);

        requestModelValidationMessageEnumerator.MoveNext();
        Assert.IsInstanceOfType<string>(requestModelValidationMessageEnumerator.Current);
        Assert.AreEqual("emailNotificationRequest.FromAssembly:null", requestModelValidationMessageEnumerator.Current);

        requestModelValidationMessageEnumerator.MoveNext();
        Assert.IsInstanceOfType<string>(requestModelValidationMessageEnumerator.Current);
        Assert.AreEqual("emailNotificationRequest.TemplateNameWithoutExtension:null", requestModelValidationMessageEnumerator.Current);
    }

    [TestMethod]
    public async Task EmailNotificationToServerService_SendNotification_EmptySendToValues_ThrowsValidationException(
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            [],
            "Subject",
            Assembly.GetExecutingAssembly(),
            "Email"
        );

        // Actions
        async Task action() => await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var requestModelValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(requestModelValidationMessage);
        Assert.AreEqual("emailNotificationRequest.SendTo:empty", requestModelValidationMessage);
    }

    [TestMethod]
    public async Task EmailNotificationToServerService_SendNotification_InvalidOneOfTheSendToValues_ThrowsValidationException(
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            ["dummy"],
            "Subject",
            Assembly.GetExecutingAssembly(),
            "Email"
        );

        // Actions
        async Task action() => await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var requestModelValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(requestModelValidationMessage);
        Assert.AreEqual($"emailNotificationRequest.SendTo[{emailNotificationRequest.SendTo.First()}]:not-email", requestModelValidationMessage);
    }

    [DataTestMethod]
    [DataRow(null, "null")]
    [DataRow("  ", "whitespaces")]
    [DataRow("", "empty")]
    public async Task EmailNotificationToServerService_SendNotification_InvalidSubjectValues_ThrowsValidationException(
        string? value,
        string validationSuffix
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            ["dummy@test.com"],
#pragma warning disable CS8604 // Possible null reference argument.
            value,
#pragma warning restore CS8604 // Possible null reference argument.
            Assembly.GetExecutingAssembly(),
            "Email"
        );

        // Actions
        async Task action() => await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var requestModelValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(requestModelValidationMessage);
        Assert.AreEqual($"emailNotificationRequest.Subject:{validationSuffix}", requestModelValidationMessage);
    }

    [DataTestMethod]
    [DataRow(null, "null")]
    [DataRow("  ", "whitespaces")]
    [DataRow("", "empty")]
    public async Task EmailNotificationToServerService_SendNotification_InvalidTemplateNameWithoutExtensionValues_ThrowsValidationException(
        string? value,
        string validationSuffix
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            ["dummy@test.com"],
            "Subject",
            Assembly.GetExecutingAssembly(),
#pragma warning disable CS8604 // Possible null reference argument.
            value
#pragma warning restore CS8604 // Possible null reference argument.
        );

        // Actions
        async Task action() => await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var requestModelValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(requestModelValidationMessage);
        Assert.AreEqual($"emailNotificationRequest.TemplateNameWithoutExtension:{validationSuffix}", requestModelValidationMessage);
    }

    [DataTestMethod]
    [DataRow(null, "null")]
    [DataRow("  ", "whitespaces")]
    [DataRow("", "empty")]
    public async Task EmailNotificationToServerService_SendNotification_InvalidTemplatePathValues_ThrowsValidationException(
        string? value,
        string validationSuffix
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            ["dummy@test.com"],
            "Subject",
            Assembly.GetExecutingAssembly(),
            "Email",
#pragma warning disable CS8604 // Possible null reference argument.
            templatePath: value
#pragma warning restore CS8604 // Possible null reference argument.
        );

        // Actions
        async Task action() => await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<ValidationException>(action);
        Assert.AreEqual(1, validationException.ValidationMessages.Count());

        var requestModelValidationMessage = validationException.ValidationMessages.First();
        Assert.IsInstanceOfType<string>(requestModelValidationMessage);
        Assert.AreEqual($"emailNotificationRequest.TemplatePath:{validationSuffix}", requestModelValidationMessage);
    }

    [TestMethod]
    public async Task EmailNotificationToServerService_SendNotification_NoTemplate_ThrowsEmailTemplateNotFoundException(
    )
    {
        // Arrange
        var emailNotificationService = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            ["dummy@test.com"],
            "Subject",
            Assembly.GetExecutingAssembly(),
            "Dummy"
        );

        // Actions
        async Task action() => await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var validationException = await Assert.ThrowsExceptionAsync<EmailTemplateNotFoundException>(action);
        Assert.AreEqual($"Email Template '{emailNotificationRequest.TemplatePath}/{emailNotificationRequest.TemplateNameWithoutExtension}' not found", validationException.Message);
    }
}