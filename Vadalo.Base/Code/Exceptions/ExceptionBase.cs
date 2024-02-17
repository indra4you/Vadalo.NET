using System;

namespace Vadalo;

public abstract class ExceptionBase : Exception
{
    public ExceptionBase(
        string message
    ) : base(
        message 
    )
    { }

    public ExceptionBase(
        string message,
        Exception innerException
    ) : base(
        message,
        innerException
    )
    { }
}