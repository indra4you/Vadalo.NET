using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Vadalo.Notification;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class EmailNotificationToDirectoryServiceUnitTests
{
    private static (EmailNotificationToDirectoryService emailNotificationService, string directoryPath)  Arrange(
    )
    {
        var emailNotificationOptions = new EmailNotificationToDirectoryOptions()
        {
            ServerRootUrl = "http://localhost/",
            FromEmailAddress = "support@vadalo.in",
            DirectoryLocation = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())
        };

        var emailNotificationService = new EmailNotificationToDirectoryService(
            emailNotificationOptions
        );

        return (emailNotificationService, emailNotificationOptions.DirectoryLocation);
    }

    [TestMethod]
    public async Task EmailNotificationToDirectoryService_SendNotification_NullRequest_ThrowsValidationException(
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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
    public async Task EmailNotificationToDirectoryService_SendNotification_NullAllRequestValues_ThrowsValidationException(
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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
    public async Task EmailNotificationToDirectoryService_SendNotification_EmptySendToValues_ThrowsValidationException(
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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
    public async Task EmailNotificationToDirectoryService_SendNotification_InvalidOneOfTheSendToValues_ThrowsValidationException(
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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
    public async Task EmailNotificationToDirectoryService_SendNotification_InvalidSubjectValues_ThrowsValidationException(
        string? value,
        string validationSuffix
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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
    public async Task EmailNotificationToDirectoryService_SendNotification_InvalidTemplateNameWithoutExtensionValues_ThrowsValidationException(
        string? value,
        string validationSuffix
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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
    public async Task EmailNotificationToDirectoryService_SendNotification_InvalidTemplatePathValues_ThrowsValidationException(
        string? value,
        string validationSuffix
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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
    public async Task EmailNotificationToDirectoryService_SendNotification_NoTemplate_ThrowsEmailTemplateNotFoundException(
    )
    {
        // Arrange
        var (emailNotificationService, _) = Arrange();

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

    [TestMethod]
    public async Task EmailNotificationToDirectoryService_SendNotification_TemplateExists_SendsEmailSuccessfully(
    )
    {
        // Arrange
        var (emailNotificationService, directoryLocation) = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            ["dummy@test.com"],
            "Subject",
            Assembly.GetExecutingAssembly(),
            "Email"
        );

        Directory
            .CreateDirectory(
                directoryLocation
            );

        // Actions
        await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var allEmailFiles = Directory
            .GetFiles(
                directoryLocation,
                "*.eml"
            );
        Assert.AreEqual(1, allEmailFiles.Length);

        // Clean-up
        File.Delete(allEmailFiles.First());
        Directory.Delete(directoryLocation, false);
    }

    [TestMethod]
    public async Task EmailNotificationToDirectoryService_SendNotification_TemplateExists_EmailContainsKeyValueSuccessfully(
    )
    {
        // Arrange
        var (emailNotificationService, directoryLocation) = Arrange();

        var emailNotificationRequest = new EmailNotificationRequest(
            ["dummy@test.com"],
            "Subject",
            Assembly.GetExecutingAssembly(),
            "Email",
            new Dictionary<string, string>()
            {
                ["theKey"] = "'Value'"
            }
        );

        Directory
            .CreateDirectory(
                directoryLocation
            );

        // Actions
        await emailNotificationService
            .SendNotification(
                emailNotificationRequest
            );

        // Assertions
        var allEmailFiles = Directory
            .GetFiles(
                directoryLocation,
                "*.eml"
            );
        Assert.AreEqual(1, allEmailFiles.Length);

        var firstEmailFile = allEmailFiles.First();
        var emailContent = await File
            .ReadAllTextAsync(
                firstEmailFile
            );

        // Clean-up
        File.Delete(firstEmailFile);
        Directory.Delete(directoryLocation, false);

        // Keep this to be last assert so clean-up completes successfully
        Assert.IsTrue(emailContent.Contains(emailNotificationRequest.KeyValues!.Values.First()));
    }
}