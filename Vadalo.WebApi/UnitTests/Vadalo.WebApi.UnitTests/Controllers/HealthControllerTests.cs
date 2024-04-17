using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Vadalo.Web.Api.Controllers;

[TestClass]
[TestCategory("Unit Tests")]
public class HealthControllerTests
{
    [TestMethod]
    public async Task HealthController_Get_HappyPath_ShouldHaveValidResponse(
    )
    {
        var mockHealthCheckService = new Mock<Vadalo.HealthCheck.IHealthCheckService>(MockBehavior.Strict);
        mockHealthCheckService
            .Setup(
                expression => expression.CheckHealth(default)
            )
            .ReturnsAsync(
                new Vadalo.HealthCheck.HealthReport(
                    [],
                    Vadalo.HealthCheck.HealthStatus.Healthy,
                    TimeSpan.FromSeconds(0)
                )
            )
            .Verifiable();

        var healthCheckController = new HealthController();

        // Actions
        var response = await healthCheckController
            .Get(
                mockHealthCheckService.Object
            );

        // Assertions
        Assert.IsNotNull(response);
        Assert.IsInstanceOfType<OkObjectResult>(response);

        var castedResponse = response as OkObjectResult;
        Assert.IsNotNull(castedResponse);
        Assert.AreEqual(200, castedResponse.StatusCode);
        Assert.IsNotNull(castedResponse.Value);
        Assert.IsInstanceOfType<ViewModel.ActionResponse<Vadalo.HealthCheck.HealthReport>>(castedResponse.Value);

        var actionResponse = castedResponse.Value as ViewModel.ActionResponse<Vadalo.HealthCheck.HealthReport>;
        Assert.IsNotNull(actionResponse);
        Assert.AreEqual(ViewModel.ActionStatus.Successful, actionResponse.Status);
        Assert.AreEqual($"Health Check Status is '{Vadalo.HealthCheck.HealthStatus.Healthy}'", actionResponse.Message);

        Assert.IsNotNull(actionResponse.Data);
        Assert.AreEqual(Vadalo.HealthCheck.HealthStatus.Healthy, actionResponse.Data.Status);
        Assert.AreEqual(Vadalo.HealthCheck.HealthStatus.Healthy.ToString(), actionResponse.Data.StatusDescription);
        Assert.AreEqual(TimeSpan.Zero, actionResponse.Data.TotalDuration);
        Assert.IsNotNull(actionResponse.Data.Entries);
        Assert.AreEqual(0, actionResponse.Data.Entries.Count);
    }
}