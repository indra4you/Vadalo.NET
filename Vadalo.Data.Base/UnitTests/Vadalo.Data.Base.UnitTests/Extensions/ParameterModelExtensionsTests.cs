using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Vadalo.Data;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class ParameterModelExtensionsTests : IDataProviderTests
{
    [DataTestMethod]
    [DataRow(null, null, null)]
    [DataRow("parameterName", "parameterValue", DbType.String)]
    [DataRow("parameterName", 123, DbType.Int32)]
    [DataRow("parameterName", 123.456, DbType.Double)]
    [DataRow("parameterName", false, DbType.Boolean)]
    public void ParameterModelExtensions_AddParameter_WithModelAndDbType(
        string parameterName,
        object parameterValue,
        DbType parameterType
    )
    {
        // Arrange
        var dataProvider = this.ArrangeDataProvider();

        // Actions
        var parameters = dataProvider.Object
            .CreateParameters()
            .AddParameter(
                new ParameterModel(
                    parameterName,
                    parameterValue,
                    parameterType
                )
            );

        // Assertions
        Assert.IsNotNull(parameters);
        Assert.AreEqual(1, parameters.Count);
        Assert.AreEqual(parameterName, parameters[0].Name);
        Assert.AreEqual(parameterValue, parameters[0].Value);
        Assert.AreEqual(parameterType, parameters[0].DbType);
    }

    [DataTestMethod]
    [DataRow(null, null)]
    [DataRow("parameterName", "parameterValue")]
    [DataRow("parameterName", 123)]
    [DataRow("parameterName", 123.456)]
    [DataRow("parameterName", false)]
    public void ParameterModelExtensions_AddParameter_WithModelAndNoDbType(
        string parameterName,
        object parameterValue
    )
    {
        // Arrange
        var dataProvider = this.ArrangeDataProvider();

        // Actions
        var parameters = dataProvider.Object
            .CreateParameters()
            .AddParameter(
                new ParameterModel(
                    parameterName,
                    parameterValue
                )
            );

        // Assertions
        Assert.IsNotNull(parameters);
        Assert.AreEqual(1, parameters.Count);
        Assert.AreEqual(parameterName, parameters[0].Name);
        Assert.AreEqual(parameterValue, parameters[0].Value);
        Assert.AreEqual(null, parameters[0].DbType);
    }

    [DataTestMethod]
    [DataRow(null, null, null)]
    [DataRow("parameterName", "parameterValue", DbType.String)]
    [DataRow("parameterName", 123, DbType.Int32)]
    [DataRow("parameterName", 123.456, DbType.Double)]
    [DataRow("parameterName", false, DbType.Boolean)]
    public void ParameterModelExtensions_AddParameter_WithDbType(
        string parameterName,
        object parameterValue,
        DbType parameterType
    )
    {
        // Arrange
        var dataProvider = this.ArrangeDataProvider();

        // Actions
        var parameters = dataProvider.Object
            .CreateParameters()
            .AddParameter(
                parameterName,
                parameterValue,
                parameterType
            );

        // Assertions
        Assert.IsNotNull(parameters);
        Assert.AreEqual(1, parameters.Count);
        Assert.AreEqual(parameterName, parameters[0].Name);
        Assert.AreEqual(parameterValue, parameters[0].Value);
        Assert.AreEqual(parameterType, parameters[0].DbType);
    }

    [DataTestMethod]
    [DataRow(null, null)]
    [DataRow("parameterName", "parameterValue")]
    [DataRow("parameterName", 123)]
    [DataRow("parameterName", 123.456)]
    [DataRow("parameterName", false)]
    public void ParameterModelExtensions_AddParameter_WithNoDbType(
        string parameterName,
        object parameterValue
    )
    {
        // Arrange
        var dataProvider = this.ArrangeDataProvider();

        // Actions
        var parameters = dataProvider.Object
            .CreateParameters()
            .AddParameter(
                parameterName,
                parameterValue
            );

        // Assertions
        Assert.IsNotNull(parameters);
        Assert.AreEqual(1, parameters.Count);
        Assert.AreEqual(parameterName, parameters[0].Name);
        Assert.AreEqual(parameterValue, parameters[0].Value);
        Assert.AreEqual(null, parameters[0].DbType);
    }
}