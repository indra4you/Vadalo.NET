namespace Vadalo.Identity.Providers;

public interface IPasswordProvider
{
    (
        string oneTimePassword,
        string passwordHash
    ) GeneratePassword(
    );
}