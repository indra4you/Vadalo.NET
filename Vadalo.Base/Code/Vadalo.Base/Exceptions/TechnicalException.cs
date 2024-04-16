using System;

namespace Vadalo;

public abstract class TechnicalException : ExceptionBase
{
    public TechnicalException(
        string message
    ) : base(
        message
    )
    { }

    public TechnicalException(
        string message,
        Exception innerException
    ) : base(
        message,
        innerException
    )
    { }
}