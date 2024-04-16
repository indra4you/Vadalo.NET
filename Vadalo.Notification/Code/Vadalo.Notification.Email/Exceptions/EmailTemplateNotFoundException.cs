using System;

namespace Vadalo.Notification;

public sealed class EmailTemplateNotFoundException(
    string message,
    Exception innerException
) : TechnicalException(
    message,
    innerException
)
{ }