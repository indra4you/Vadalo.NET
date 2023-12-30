using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Vadalo;

[TestClass]
[TestCategory("Unit Tests")]
public class ValidationExtensionsTests
{
    [DataTestMethod]
    [DataRow(null, true, false)]
    [DataRow("", true, false)]
    [DataRow("", false, false)]
    [DataRow(" ", true, false)]
    [DataRow(" ", false, true)]
    [DataRow("abc", true, true)]
    [DataRow("abc", false, true)]
    [DataRow(" abc ", false, true)]
    [DataRow(" abc ", true, true)]
    public void ValidationExtensions_HaveValue_String(
        string? inputValue,
        bool trimWhiteSpace,
        bool expectedResponse
    )
    {
        // Arrange & Actions
        var result = inputValue.HaveValue(trimWhiteSpace);

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResponse, result);
    }

    [DataTestMethod]
    [DataRow(null, true, true)]
    [DataRow("", true, true)]
    [DataRow("", false, true)]
    [DataRow(" ", true, true)]
    [DataRow(" ", false, false)]
    [DataRow("abc", true, false)]
    [DataRow("abc", false, false)]
    [DataRow(" abc ", false, false)]
    [DataRow(" abc ", true, false)]
    public void ValidationExtensions_HaveNoValue_String(
        string? inputValue,
        bool trimWhiteSpace,
        bool expectedResponse
    )
    {
        // Arrange & Actions
        var result = inputValue.HaveNoValue(trimWhiteSpace);

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResponse, result);
    }

    [TestMethod]
    public void ValidationExtensions_HaveValues_HappyPath(
    )
    {
        // Arrange
        var values = new List<int>
        {
            0,
            1,
            2
        };

        // Actions
        var result = values.HaveValues();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void ValidationExtensions_HaveValues_Empty(
    )
    {
        // Arrange
        var values = new List<int> { };

        // Actions
        var result = values.HaveValues();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void ValidationExtensions_HaveValues_Null(
    )
    {
        // Arrange
        List<int>? values = null;

        // Actions
        var result = values.HaveValues();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void ValidationExtensions_HaveNoValues_HappyPath(
    )
    {
        // Arrange
        var values = new List<int>
        {
            0,
            1,
            2
        };

        // Actions
        var result = values.HaveNoValues();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void ValidationExtensions_HaveNoValues_Empty(
    )
    {
        // Arrange
        var values = new List<int> { };

        // Actions
        var result = values.HaveNoValues();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void ValidationExtensions_HaveNoValues_Null(
    )
    {
        // Arrange
        List<int>? values = null;

        // Actions
        var result = values.HaveNoValues();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }
}