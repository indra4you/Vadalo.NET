using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vadalo.Web.Api.Controllers;

[TestClass]
[TestCategory("Unit Tests")]
public class HealthControllerTests
{
    [TestMethod]
    public async Task HealthController_Get_EmptyHealthCheck_ShouldHaveValidResponse(
    )
    {
        // Arrange
        var mockHealthCheckService = new Mock<HealthCheck.IHealthCheckService>(
            MockBehavior.Strict
        );
        mockHealthCheckService
            .Setup(
                expression => expression.CheckHealth(default)
            )
            .ReturnsAsync(
                new HealthCheck.Models.HealthReport(
                    new List<HealthCheck.Models.HealthReportEntry>(),
                    HealthCheck.HealthStatus.Healthy,
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

        var castedResponse = (OkObjectResult)response;
        Assert.IsNotNull(castedResponse);
        Assert.AreEqual(200, castedResponse.StatusCode);
        Assert.IsNotNull(castedResponse.Value);
        Assert.IsInstanceOfType<ViewModel.ActionResponse<HealthCheck.Models.HealthReport>>(castedResponse.Value);

        var actionResponse = (ViewModel.ActionResponse<HealthCheck.Models.HealthReport>)castedResponse.Value;
        Assert.IsNotNull(actionResponse);
        Assert.AreEqual(ViewModel.ActionStatus.Successful, actionResponse.Status);
        Assert.AreEqual($"Health Check Status is '{HealthCheck.HealthStatus.Healthy}'", actionResponse.Message);

        Assert.IsNotNull(actionResponse.Data);
        Assert.AreEqual(HealthCheck.HealthStatus.Healthy, actionResponse.Data.Status);
        Assert.AreEqual(HealthCheck.HealthStatus.Healthy.ToString(), actionResponse.Data.StatusDescription);
        Assert.AreEqual(TimeSpan.Zero, actionResponse.Data.TotalDuration);
        Assert.IsNotNull(actionResponse.Data.Entries);
        Assert.AreEqual(0, actionResponse.Data.Entries.Count);
    }
}