using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Vadalo;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class ExtensionsTests
{
    #region String

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
    public void Extensions_HasNoValue_String(
        string? inputValue,
        bool trimWhiteSpace,
        bool expectedResponse
    )
    {
        // Arrange & Actions
        var result = inputValue.HasNoValue(trimWhiteSpace);

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResponse, result);
    }

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
    public void Extensions_HasValue_String(
        string? inputValue,
        bool trimWhiteSpace,
        bool expectedResponse
    )
    {
        // Arrange & Actions
        var result = inputValue.HasValue(trimWhiteSpace);

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResponse, result);
    }

    #endregion

    #region Nullable Guid

    [TestMethod]
    public void Extensions_HasNoValue_NullableGuid_Null(
    )
    {
        // Arrange & Actions
        Guid? inputValue = null;
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Extensions_HasNoValue_NullableGuid_Empty(
    )
    {
        // Arrange & Actions
        Guid? inputValue = Guid.Empty;
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Extensions_HasNoValue_NullableGuid_WithValue(
    )
    {
        // Arrange & Actions
        Guid? inputValue = Guid.NewGuid();
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasValue_NullableGuid_Null(
    )
    {
        // Arrange & Actions
        Guid? inputValue = null;
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasValue_NullableGuid_Empty(
    )
    {
        // Arrange & Actions
        Guid? inputValue = Guid.Empty;
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasValue_NullableGuid_WithValue(
    )
    {
        // Arrange & Actions
        Guid? inputValue = Guid.NewGuid();
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    #endregion

    #region Guid

    [TestMethod]
    public void Extensions_HasNoValue_Guid_Empty(
    )
    {
        // Arrange & Actions
        var inputValue = Guid.Empty;
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Extensions_HasNoValue_Guid_WithValue(
    )
    {
        // Arrange & Actions
        var inputValue = Guid.NewGuid();
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasValue_Guid_Empty(
    )
    {
        // Arrange & Actions
        var inputValue = Guid.Empty;
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasValue_Guid_WithValue(
    )
    {
        // Arrange & Actions
        var inputValue = Guid.NewGuid();
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    #endregion

    #region IEnumerable

    [TestMethod]
    public void Extensions_HasNoValue_IEnumerable_Null(
    )
    {
        // Arrange & Actions
        List<string>? inputValue = null;
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Extensions_HasValue_IEnumerable_Null(
    )
    {
        // Arrange & Actions
        List<string>? inputValue = null;
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasNoValue_IEnumerable_Empty(
    )
    {
        // Arrange & Actions
        var inputValue = new List<string>();
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void Extensions_HasNoValue_IEnumerable_WithValue(
    )
    {
        // Arrange & Actions
        var inputValue = new List<string>() { "Dummy" };
        var result = inputValue.HasNoValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasValue_IEnumerable_Empty(
    )
    {
        // Arrange & Actions
        var inputValue = new List<string>();
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Extensions_HasValue_IEnumerable_WithValue(
    )
    {
        // Arrange & Actions
        var inputValue = new List<string>() { "Dummy" };
        var result = inputValue.HasValue();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(true, result);
    }

    #endregion

    #region EmailAddress

    [DataTestMethod]
    [DataRow("", false)]
    [DataRow("a", false)]
    [DataRow("A", false)]
    [DataRow("a@", false)]
    [DataRow("A@", false)]
    [DataRow("a@b", false)]
    [DataRow("A@B", false)]
    [DataRow("a@b.", false)]
    [DataRow("A@B.", false)]
    [DataRow("a@b.c", true)]
    [DataRow("A@B.C", true)]
    [DataRow("€@B.C", false)]
    [DataRow("test@domain.com", true)]
    [DataRow("123@domain.com", true)]
    public void Extensions_IsEmailAddress(
        string inputValue,
        bool expectedResponse
    )
    {
        // Arrange & Actions
        var result = inputValue.IsEmailAddress();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResponse, result);
    }

    [DataTestMethod]
    [DataRow("", true)]
    [DataRow("a", true)]
    [DataRow("A", true)]
    [DataRow("a@", true)]
    [DataRow("A@", true)]
    [DataRow("a@b", true)]
    [DataRow("A@B", true)]
    [DataRow("a@b.", true)]
    [DataRow("A@B.", true)]
    [DataRow("a@b.c", false)]
    [DataRow("A@B.C", false)]
    [DataRow("€@B.C", true)]
    [DataRow("test@domain.com", false)]
    [DataRow("123@domain.com", false)]
    public void Extensions_IsNotEmailAddress(
        string inputValue,
        bool expectedResponse
    )
    {
        // Arrange & Actions
        var result = inputValue.IsNotEmailAddress();

        // Assertions
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResponse, result);
    }

    #endregion
}