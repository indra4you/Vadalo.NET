namespace Vadalo;

public abstract class TechnicalException : ExceptionBase
{
    public TechnicalException(
        string message
    ) : base(
        message
    )
    { }
}