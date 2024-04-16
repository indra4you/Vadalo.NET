using System.Collections.Generic;

namespace Vadalo;

public static class DataProviderExtensions
{
    public static IList<Data.ParameterModel> CreateParameters(
        this Data.IDataProvider _
    ) =>
        [];
}