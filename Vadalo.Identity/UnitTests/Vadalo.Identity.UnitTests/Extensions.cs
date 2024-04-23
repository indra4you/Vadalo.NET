using Moq;
using System;
using System.Threading.Tasks;

namespace Vadalo.Identity;

internal static class Extensions
{
    internal const string INVITEE_EMAIL_ADDRESS = "invitee@domain.com";
    internal const string INVITED_BY_EMAIL_ADDRESS = "invited-by@domain.com";

    internal static Providers.IdentityNodeModel MockIdentityNodeModel(
        this IIdentityServiceTests _,
        string? signInID = null,
        Guid? invitedBy = null,
        Guid? id = null
    ) =>
        new(
            id ?? Guid.NewGuid(),
            invitedBy ?? Guid.NewGuid(),
            signInID ?? INVITEE_EMAIL_ADDRESS
        );

    internal static Providers.MemberNodeModel MockMemberNodeModel(
        this IIdentityServiceTests _,
        string? emailAddress = null,
        string? lastName = null,
        string? firstName = null,
        byte? status = null,
        Guid? id = null,
        string? middleName = null
    ) =>
        new(
            id ?? Guid.NewGuid(),
            status ?? 0,
            emailAddress ?? INVITED_BY_EMAIL_ADDRESS,
            lastName ?? "Last",
            firstName ?? "First",
            middleName
        );

    internal static string ToDisplayName(
        this Providers.MemberNodeModel memberNodeModel
    ) =>
        memberNodeModel.MiddleName.HasNoValue()
            ? string
                .Join(
                    ' ',
                    memberNodeModel.FirstName,
                    memberNodeModel.LastName
                )
            : string
                .Join(
                    ' ',
                    memberNodeModel.FirstName,
                    memberNodeModel.MiddleName,
                    memberNodeModel.LastName
                );

    internal static Providers.IdentityOfEdgeModel MockIdentityOfEdgeModel(
        this IIdentityServiceTests identityServiceTests,
        Providers.IdentityNodeModel? fromNode = null,
        Providers.MemberNodeModel? toNode = null,
        string? fromNodeID = null,
        string? toNodeID = null,
        string? edgeID = null
    )
    {
        var fromNodeValue = fromNode ?? identityServiceTests.MockIdentityNodeModel();
        var toNodeValue = toNode ?? identityServiceTests.MockMemberNodeModel();

        return new(
            edgeID ?? $"edgeID-{Guid.NewGuid()}",
            fromNodeID ?? $"fromNodeID-{fromNodeValue.ID}",
            toNodeID ?? $"toNodeID-{toNodeValue.Id}",
            fromNodeValue,
            toNodeValue
        );
    }

    internal static Mock<Providers.IIdentityDataProvider> ArrangeIdentityDataProvider(
        this IIdentityServiceTests _,
        Providers.IdentityOfEdgeModel? mockIdentityOfEdgeModel = null,
        Providers.IdentityNodeModel? identityNodeModel = null
    )
    {
        var mockIdentityDataProvider = new Mock<Providers.IIdentityDataProvider>(
            MockBehavior.Strict
        );
        mockIdentityDataProvider
            .Setup(
                expression => expression.FetchIdentityOfEdgeByEmailAddress(It.IsAny<string>())
            )
            .ReturnsAsync(
                mockIdentityOfEdgeModel
            )
            .Verifiable();
        mockIdentityDataProvider
            .Setup(
                expression => expression.FetchIdentityNodeBySignInID(It.IsAny<string>())
            )
            .ReturnsAsync(
                identityNodeModel
            )
            .Verifiable();
        mockIdentityDataProvider
            .Setup(
                expression => expression.CreateIdentityNode(It.IsAny<Guid>(), It.IsAny<string>())
            )
            .Returns(
                Task.CompletedTask
            )
            .Verifiable();
        mockIdentityDataProvider
            .Setup(
                expression => expression.CreatePassHashNode(It.IsAny<Guid>(), It.IsAny<string>())
            )
            .Returns(
                Task.CompletedTask
            )
            .Verifiable();

        return mockIdentityDataProvider;
    }

    internal static Mock<Providers.IPasswordProvider> ArrangePasswordProvider(
        this IIdentityServiceTests _,
        string mockOneTimePassword = "000000",
        string mockPasswordHash = "passwordHash"
    )
    {
        var mockPasswordProvider = new Mock<Providers.IPasswordProvider>(
            MockBehavior.Strict
        );
        mockPasswordProvider
            .Setup(
                expression => expression.GeneratePassword()
            )
            .Returns(
                (
                    mockOneTimePassword,
                    mockPasswordHash
                )
            )
            .Verifiable();

        return mockPasswordProvider;
    }

    internal static Mock<Providers.IEmailNotificationProvider> ArrangeEmailNotificationProvider(
        this IIdentityServiceTests _
    )
    {
        var mockEmailNotificationProvider = new Mock<Providers.IEmailNotificationProvider>(
            MockBehavior.Strict
        );
        mockEmailNotificationProvider
            .Setup(
                expression => expression.SendInvitation(It.IsAny<Providers.InviteNotificationModel>())
            )
            .Returns(
                Task.CompletedTask
            )
            .Verifiable();
        mockEmailNotificationProvider
            .Setup(
                expression => expression.SendOneTimePassword(It.IsAny<Providers.OneTimePasswordNotificationModel>())
            )
            .Returns(
                Task.CompletedTask
            )
            .Verifiable();

        return mockEmailNotificationProvider;
    }
}