namespace Vadalo.Identity;

public sealed class IdentityOfEdgeNotFoundException(
    string message
) : TechnicalException(
    message
)
{ }