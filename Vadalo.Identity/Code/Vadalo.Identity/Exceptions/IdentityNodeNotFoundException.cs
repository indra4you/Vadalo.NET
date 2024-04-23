namespace Vadalo.Identity;

public sealed class IdentityNodeNotFoundException(
    string message
) : TechnicalException(
    message
)
{ }