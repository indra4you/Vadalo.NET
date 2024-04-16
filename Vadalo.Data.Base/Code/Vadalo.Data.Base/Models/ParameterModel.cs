using System.Data;

namespace Vadalo.Data;

public sealed class ParameterModel(
    string name,
    object value,
    DbType? dbType = null
)
{
    public string Name = name;

    public object Value = value;

    public DbType? DbType = dbType;
}