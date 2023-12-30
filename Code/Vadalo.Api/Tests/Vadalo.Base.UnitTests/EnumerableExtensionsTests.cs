using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Vadalo;

[TestClass]
[TestCategory("Unit Tests")]
public class EnumerableExtensionsTests
{
    [TestMethod]
    public void EnumerableExtensions_ForEach_WithIndex_HappyPath(
    )
    {
        // Arrange
        var values = new List<int>
        {
            0,
            1,
            2
        };

        // Actions & Assertions
        var result = values
            .ForEach(
                (value, index) =>
                {
                    Assert.AreEqual(value, index);

                    return $"{index}.{value}";
                }
            )
            .ToList();

        Assert.IsNotNull(result);
        Assert.AreEqual(values.Count, result.Count);

        for ( int i = 0; i < result.Count; i++ )
            Assert.AreEqual($"{values[i]}.{i}", result[i]);
    }

    [TestMethod]
    public void EnumerableExtensions_ForEach_WithIndex_Empty(
    )
    {
        // Arrange
        var values = new List<int> { };

        // Actions & Assertions
        var result = values
            .ForEach(
                (value, index) => $"{index}.{value}"
            )
            .ToList();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void EnumerableExtensions_ForEach_WithIndex_TypesValidations(
    )
    {
        // Arrange
        var values = new List<int>
        {
            0,
            1,
            2
        };

        // Actions & Assertions
        var result = values
            .ForEach(
                (value, index) =>
                {
                    Assert.IsInstanceOfType<int>(value);
                    Assert.IsInstanceOfType<int>(index);

                    return $"{index}.{value}";
                }
            )
            .ToList();

        Assert.IsInstanceOfType<List<string>>(result);
    }

    [TestMethod]
    public void EnumerableExtensions_ForEach_WithoutIndex_HappyPath(
    )
    {
        // Arrange
        var values = new List<int>
        {
            0,
            1,
            2
        };

        // Actions & Assertions
        var result = values
            .ForEach(
                value => $"{value}.{value}"
            )
            .ToList();

        Assert.IsNotNull(result);
        Assert.AreEqual(values.Count, result.Count);

        for (int i = 0; i < result.Count; i++)
            Assert.AreEqual($"{values[i]}.{values[i]}", result[i]);
    }

    [TestMethod]
    public void EnumerableExtensions_ForEach_WithoutIndex_Empty(
    )
    {
        // Arrange
        var values = new List<int> { };

        // Actions & Assertions
        var result = values
            .ForEach(
                value => $"{value}.{value}"
            )
            .ToList();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void EnumerableExtensions_ForEach_WithOutIndex_TypesValidations(
    )
    {
        // Arrange
        var values = new List<int>
        {
            0,
            1,
            2
        };

        // Actions & Assertions
        var result = values
            .ForEach(
                value =>
                {
                    Assert.IsInstanceOfType<int>(value);

                    return $"{value}.{value}";
                }
            )
            .ToList();

        Assert.IsInstanceOfType<List<string>>(result);
    }
}