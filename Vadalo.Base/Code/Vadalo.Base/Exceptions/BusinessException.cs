using System;

namespace Vadalo;

public abstract class BusinessException : ExceptionBase
{
    public BusinessException(
        string message
    ) : base(
        message
    )
    { }

    public BusinessException(
        string message,
        Exception innerException
    ) : base(
        message,
        innerException
    )
    { }
}