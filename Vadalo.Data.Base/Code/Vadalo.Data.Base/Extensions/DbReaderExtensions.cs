using System;
using System.Data.Common;

namespace Vadalo;

public static class DbReaderExtensions
{
    private static int GetConfirmedOrdinal(
        this DbDataReader dataReader,
        string columnName
    )
    {
        var columnOrdinal = dataReader
            .GetOrdinal(
                columnName
            );
        if (-1 == columnOrdinal)
            throw new Data.DataColumnNotFoundException(
                $"Data column '{columnName}' not found in the data reader",
                columnName
            );

        return columnOrdinal;
    }

    public static Guid GetGuid(
        this DbDataReader dataReader,
        string columnName
    ) =>
        dataReader
            .GetGuid(
                 dataReader
                    .GetConfirmedOrdinal(
                        columnName
                    )
            );

    public static Guid? GetNullableGuid(
        this DbDataReader dataReader,
        string columnName
    )
    {
        var columnOrdinal = dataReader
            .GetConfirmedOrdinal(
                columnName
            );
        if (dataReader.IsDBNull(columnOrdinal))
            return null;

        return dataReader
            .GetGuid(
                columnOrdinal
            );
    }

    public static string GetString(
        this DbDataReader dataReader,
        string columnName
    ) =>
        dataReader
            .GetString(
                dataReader
                    .GetConfirmedOrdinal(
                        columnName
                    )
            );

    public static string? GetNullableString(
        this DbDataReader dataReader,
        string columnName
    )
    {
        var columnOrdinal = dataReader
            .GetConfirmedOrdinal(
                columnName
            );
        if (dataReader.IsDBNull(columnOrdinal))
            return null;

        return dataReader
            .GetString(
                columnOrdinal
            );
    }

    public static byte GetByte(
        this DbDataReader dataReader,
        string columnName
    ) =>
        dataReader
            .GetByte(
                 dataReader
                    .GetConfirmedOrdinal(
                        columnName
                    )
            );

    public static byte? GetNullableByte(
        this DbDataReader dataReader,
        string columnName
    )
    {
        var columnOrdinal = dataReader
            .GetConfirmedOrdinal(
                columnName
            );
        if (dataReader.IsDBNull(columnOrdinal))
            return null;

        return dataReader
            .GetByte(
                columnOrdinal
            );
    }
}