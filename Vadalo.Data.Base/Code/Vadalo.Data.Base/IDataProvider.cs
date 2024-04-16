using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Vadalo.Data;

public interface IDataProvider
{
    Task<int> ExecuteNonQuery(
        string sqlStatement,
        IEnumerable<ParameterModel> parameters
    );

    Task<int> ExecuteNonQuery(
        string sqlStatement
    );

    Task<object?> ExecuteScalar(
        string sqlStatement,
        IEnumerable<ParameterModel> parameters
    );

    Task<object?> ExecuteScalar(
        string sqlStatement
    );

    Task<IEnumerable<T>> ExecuteReader<T>(
        string sqlStatement,
        IEnumerable<ParameterModel> parameters,
        Func<DbDataReader, T> modelMapperMethod
    );

    Task<IEnumerable<T>> ExecuteReader<T>(
        string sqlStatement,
        Func<DbDataReader, T> modelMapperMethod
    );
}