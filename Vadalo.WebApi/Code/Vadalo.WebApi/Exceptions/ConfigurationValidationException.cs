namespace Vadalo.Web.Api;

public sealed class ConfigurationValidationException(
    string message
) : TechnicalException(
    message
)
{ }