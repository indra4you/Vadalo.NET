namespace Vadalo.Identity.Providers;

public interface IPasswordProvider
{
    (
        string oneTimePassword,
        string passwordHash
    ) GeneratePassword(
    );

    bool VerifyPassword(
        string passwordHash,
        string oneTimePassword
    );

    string GenerateJwtToken(
        IdentityOfEdgeModel identityOfEdgeModel
    );
}