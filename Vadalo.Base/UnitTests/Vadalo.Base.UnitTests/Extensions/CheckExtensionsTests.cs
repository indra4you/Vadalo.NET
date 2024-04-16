using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vadalo;

[TestClass]
[TestCategory("Unit Tests")]
public sealed class CheckExtensionsTests
{
    [TestMethod]
    public void CheckExtensions_CheckRequired_WithObjectNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        object? value = null;

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:null", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithObjectNotNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        object value = "something";

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(0, validationMessages.Count);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithEnumerableTypeNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        List<string>? value = null;

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:null", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithEnumerableTypeEmpty(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        List<string>? value = [];

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:empty", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithEnumerableTypeNotNullAndNotEmpty(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        List<string> value =
        [
            "Dummy"
        ];

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(0, validationMessages.Count);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithAssemblyNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        Assembly? value = null;

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:null", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithAssemblyNotNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        Assembly value = Assembly.GetExecutingAssembly();

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(0, validationMessages.Count);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithGuidNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        Guid? value = null;

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:null", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithGuidEmpty(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        Guid? value = Guid.Empty;

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:empty", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithGuidValue(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        Guid? value = Guid.NewGuid();

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(0, validationMessages.Count);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithStringNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        string? value = null;

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:null", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithStringEmpty(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        string? value = string.Empty;

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:empty", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithStringWhitespaces(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        string? value = "   ";

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:whitespaces", validationMessages[0]);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithStringValue(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        string? value = "dummy";

        // Actions
        validationMessages
            .CheckRequired(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(0, validationMessages.Count);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithEmailAddressNull(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        string? value = null;

        // Actions
        validationMessages
            .CheckEmailAddress(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(0, validationMessages.Count);
    }

    [TestMethod]
    public void CheckExtensions_CheckRequired_WithEmailAddressNotValid(
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";
        string? value = "abc";

        // Actions
        validationMessages
            .CheckEmailAddress(
                value,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(1, validationMessages.Count);
        Assert.AreEqual($"{validationPath}:not-email", validationMessages[0]);
    }

    [DataTestMethod]
    [DataRow("a@b.c")]
    [DataRow("A@B.C")]
    [DataRow("test@domain.com")]
    [DataRow("123@domain.com")]
    public void CheckExtensions_CheckRequired_WithEmailAddressValid(
        string inputValue
    )
    {
        // Arrange
        var validationMessages = new List<string>();
        string validationPath = "ThePath";

        // Actions
        validationMessages
            .CheckEmailAddress(
                inputValue,
                validationPath
            );

        // Assertions
        Assert.IsNotNull(validationMessages);
        Assert.AreEqual(0, validationMessages.Count);
    }
}