using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vadalo.Identity.Providers.OneTimePassword;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class GeneratePasswordTests
{
    [TestMethod]
    public void OneTimePasswordProvider_GeneratePassword_HappyPath_ShouldReturnValues(
    )
    {
        // Arrange
        var mockOneTimePasswordOptions = new OneTimePasswordOptions(
            6
        );
        var mockOneTimePasswordProvider = new OneTimePasswordProvider(
            mockOneTimePasswordOptions
        );

        // Actions
        var (oneTimePassword, passwordHash) = mockOneTimePasswordProvider
            .GeneratePassword();

        // Assertions
        Assert.IsNotNull(oneTimePassword);
        Assert.AreEqual(6, oneTimePassword.Length);
        Assert.IsNotNull(passwordHash);
    }
}