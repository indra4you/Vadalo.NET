using System.Collections.Generic;

namespace Vadalo;

public sealed class ValidationException(
    string message,
    IEnumerable<string> validationMessages
) : TechnicalException(
    message
)
{
    public IEnumerable<string> ValidationMessages = validationMessages;
}