using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Vadalo;

public static class MockDataProviderExtensions
{
    public static Mock<Data.IDataProvider> ArrangeDataProvider(
        this Data.IDataProviderTests _
    ) =>
        new(MockBehavior.Strict);

    public static Mock<Data.IDataProvider> MockExecuteNonQuery(
        this Mock<Data.IDataProvider> mockDataProvider,
        int expectedReturnValue
    )
    {
        mockDataProvider
            .Setup(
                expression => expression.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<IEnumerable<Data.ParameterModel>>())
            )
            .ReturnsAsync(
                expectedReturnValue
            )
            .Verifiable();

        return mockDataProvider;
    }

    public static Mock<Data.IDataProvider> MockExecuteNonQuery(
        this Mock<Data.IDataProvider> mockDataProvider,
        Exception exception
    )
    {
        mockDataProvider
            .Setup(
                expression => expression.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<IEnumerable<Data.ParameterModel>>())
            )
            .ThrowsAsync(
                exception
            )
            .Verifiable();

        return mockDataProvider;
    }

    public static Mock<Data.IDataProvider> ExecuteScalar(
        this Mock<Data.IDataProvider> mockDataProvider,
        object? expectedReturnValue
    )
    {
        mockDataProvider
            .Setup(
                expression => expression.ExecuteScalar(It.IsAny<string>(), It.IsAny<IEnumerable<Data.ParameterModel>>())
            )
            .ReturnsAsync(
                expectedReturnValue
            )
            .Verifiable();

        return mockDataProvider;
    }

    public static Mock<Data.IDataProvider> ExecuteScalar(
        this Mock<Data.IDataProvider> mockDataProvider,
        Exception exception
    )
    {
        mockDataProvider
            .Setup(
                expression => expression.ExecuteScalar(It.IsAny<string>(), It.IsAny<IEnumerable<Data.ParameterModel>>())
            )
            .ThrowsAsync(
                exception
            )
            .Verifiable();

        return mockDataProvider;
    }

    public static Mock<Data.IDataProvider> ExecuteReader<T>(
        this Mock<Data.IDataProvider> mockDataProvider,
        IEnumerable<T> expectedReturnValue
    )
    {
        mockDataProvider
            .Setup(
                expression => expression.ExecuteReader(It.IsAny<string>(), It.IsAny<IEnumerable<Data.ParameterModel>>(), It.IsAny<Func<DbDataReader, T>>())
            )
            .ReturnsAsync(
                expectedReturnValue
            )
            .Verifiable();

        return mockDataProvider;
    }

    public static Mock<Data.IDataProvider> ExecuteReader<T>(
        this Mock<Data.IDataProvider> mockDataProvider,
        Exception exception
    )
    {
        mockDataProvider
            .Setup(
                expression => expression.ExecuteReader(It.IsAny<string>(), It.IsAny<IEnumerable<Data.ParameterModel>>(), It.IsAny<Func<DbDataReader, T>>())
            )
            .ThrowsAsync(
                exception
            )
            .Verifiable();

        return mockDataProvider;
    }
}