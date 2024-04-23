namespace Vadalo.Data;

public sealed class SqlServerDataOptions(
    string connectionString
)
{
    public string ConnectionString { get; } = connectionString;
}