using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vadalo.Web.Api.Controllers;

[TestClass]
[TestCategory("Unit Tests")]
public class PingControllerTests
{
    [TestMethod]
    public void PingController_Get_HappyPath_ShouldHaveValidResponse(
    )
    {
        // Arrange
        var pingController = new PingController();

        // Actions
        var response = pingController.Get();

        // Assertions
        Assert.IsNotNull(response);
        Assert.IsInstanceOfType<OkObjectResult>(response);

        var castedResponse = (OkObjectResult)response;
        Assert.IsNotNull(castedResponse);
        Assert.AreEqual(castedResponse.StatusCode, 200);
        Assert.IsNotNull(castedResponse.Value);
        Assert.IsInstanceOfType<ViewModel.ActionResponse<string>>(castedResponse.Value);

        var actionResponse = (ViewModel.ActionResponse<string>)castedResponse.Value;
        Assert.IsNotNull(actionResponse);
        Assert.AreEqual(actionResponse.Status, ViewModel.ActionStatus.Successful);
        Assert.AreEqual(actionResponse.Data, "Pong");
        StringAssert.EndsWith(actionResponse.Message, " UTC] Server is up and running");
    }
}