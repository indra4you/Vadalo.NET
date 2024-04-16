namespace Vadalo.Data;

public sealed class DataColumnNotFoundException(
    string message,
    string dataColumnName
) : TechnicalException(
    message
)
{
    public string DataColumnName => dataColumnName;
}