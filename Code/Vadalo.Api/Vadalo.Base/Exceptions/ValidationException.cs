namespace Vadalo;

public class ValidationException : TechnicalException
{
    public ValidationException(
        string message
    ) : base(
        message
    )
    { }
}