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

        var castedResponse = response as OkObjectResult;
        Assert.IsNotNull(castedResponse);
        Assert.AreEqual(200, castedResponse.StatusCode);
        Assert.IsNotNull(castedResponse.Value);
        Assert.IsInstanceOfType<ViewModel.ActionResponse<string>>(castedResponse.Value);

        var actionResponse = castedResponse.Value as ViewModel.ActionResponse<string>;
        Assert.IsNotNull(actionResponse);
        Assert.AreEqual(ViewModel.ActionStatus.Successful, actionResponse.Status);
        Assert.AreEqual("Pong", actionResponse.Data);
        StringAssert.EndsWith(actionResponse.Message, " UTC] Server is up and running");
    }
}