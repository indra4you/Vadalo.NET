using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vadalo.HealthCheck;

[TestClass]
[TestCategory("Unit Tests")]
public class HealthCheckServiceTests
{
    [TestMethod]
    public async Task HealthCheckService_CheckHealth_EmptyHealthChecks_ReturnsHealthyReport(
    )
    {
        // Arrange
        var healthChecks = new List<IHealthCheck>();
        var healthCheckService = new Services.HealthCheckService(healthChecks);

        // Actions
        var healthReport = await healthCheckService
            .CheckHealth();

        // Assertions
        Assert.IsNotNull(healthReport);
        Assert.AreEqual(HealthStatus.Healthy, healthReport.Status);
        Assert.AreEqual(HealthStatus.Healthy.ToString(), healthReport.StatusDescription);
        Assert.AreEqual(TimeSpan.Zero, healthReport.TotalDuration);
        Assert.AreEqual(0, healthReport.Entries.Count);
    }

    [DataTestMethod]
    [DataRow(HealthStatus.Healthy)]
    [DataRow(HealthStatus.Degraded)]
    [DataRow(HealthStatus.Unhealthy)]
    public async Task HealthCheckService_CheckHealth_ReturnsValidHealthReport(
        HealthStatus healthStatus
    )
    {
        // Arrange
        var healthCheckFixtureName = "Health Check Test";
        var expectedHealthCheckReport = new Models.HealthCheckResult(
            healthStatus,
            $"Health Check Fixture - {healthStatus}"
        );
        var healthChecks = new List<IHealthCheck>
        {
            new HealthCheckFixture(
                healthCheckFixtureName,
                expectedHealthCheckReport
            )
        };
        var healthCheckService = new Services.HealthCheckService(
            healthChecks
        );

        // Actions
        var healthReport = await healthCheckService
            .CheckHealth();

        // Assertions
        Assert.IsNotNull(healthReport);
        Assert.AreEqual(healthStatus, healthReport.Status);
        Assert.AreEqual(healthStatus.ToString(), healthReport.StatusDescription);
        Assert.IsTrue(healthReport.TotalDuration > TimeSpan.Zero);
        Assert.AreEqual(1, healthReport.Entries.Count);

        var healthCheck = healthReport.Entries[0];
        Assert.AreEqual(healthCheckFixtureName, healthCheck.Name);
        Assert.AreEqual(expectedHealthCheckReport.Status, healthCheck.Status);
        Assert.AreEqual(expectedHealthCheckReport.Status.ToString(), healthCheck.StatusDescription);
        Assert.AreEqual(expectedHealthCheckReport.Description, healthCheck.Description);
        Assert.IsTrue(TimeSpan.FromSeconds(1) < healthCheck.Duration);
    }

    [TestMethod]
    [DataRow(HealthStatus.Healthy, HealthStatus.Healthy, HealthStatus.Healthy)]
    [DataRow(HealthStatus.Healthy, HealthStatus.Degraded, HealthStatus.Degraded)]
    [DataRow(HealthStatus.Healthy, HealthStatus.Unhealthy, HealthStatus.Unhealthy)]
    [DataRow(HealthStatus.Degraded, HealthStatus.Degraded, HealthStatus.Degraded)]
    [DataRow(HealthStatus.Degraded, HealthStatus.Healthy, HealthStatus.Degraded)]
    [DataRow(HealthStatus.Degraded, HealthStatus.Unhealthy, HealthStatus.Unhealthy)]
    [DataRow(HealthStatus.Unhealthy, HealthStatus.Unhealthy, HealthStatus.Unhealthy)]
    [DataRow(HealthStatus.Unhealthy, HealthStatus.Healthy, HealthStatus.Unhealthy)]
    [DataRow(HealthStatus.Unhealthy, HealthStatus.Degraded, HealthStatus.Unhealthy)]
    public async Task HealthCheckService_CheckHealth_MultipleHealths_ReturnsExpectedHealth(
        HealthStatus firstHealthStatus,
        HealthStatus secondHealthStatus,
        HealthStatus expectedHealthStatus
    )
    {
        // Arrange
        var healthChecks = new List<IHealthCheck>
        {
            new HealthCheckFixture(
                "First Health Check",
                new Models.HealthCheckResult(
                    firstHealthStatus,
                    $"Health Check Fixture - {firstHealthStatus}"
                )
            ),
            new HealthCheckFixture(
                "Second Health Check",
                new Models.HealthCheckResult(
                    secondHealthStatus,
                    $"Health Check Fixture - {secondHealthStatus}"
                )
            )
        };
        var healthCheckService = new Services.HealthCheckService(
            healthChecks
        );

        // Actions
        var healthReport = await healthCheckService
            .CheckHealth();

        // Assertions
        Assert.IsNotNull(healthReport);
        Assert.AreEqual(expectedHealthStatus, healthReport.Status);
        Assert.AreEqual(expectedHealthStatus.ToString(), healthReport.StatusDescription);
        Assert.AreEqual(2, healthReport.Entries.Count);

        Assert.AreEqual(firstHealthStatus, healthReport.Entries[0].Status);
        Assert.AreEqual(firstHealthStatus.ToString(), healthReport.Entries[0].StatusDescription);

        Assert.AreEqual(secondHealthStatus, healthReport.Entries[1].Status);
        Assert.AreEqual(secondHealthStatus.ToString(), healthReport.Entries[1].StatusDescription);
    }

    [DataTestMethod]
    //[DataRow(1)]
    [DataRow(2)]
    //[DataRow(3)]
    //[DataRow(5)]
    //[DataRow(10)]
    //[DataRow(20)]
    //[DataRow(50)]
    //[DataRow(100)]
    public async Task HealthCheckService_CheckHealth_Concurrent_ReturnsValidHealthReport(
        int numberHealthChecks
    )
    {
        // Arrange
        var healthCheckFixtureNamePrefix = "Health Check Test";
        var random = new Random();
        var secondsToSleepList = new List<int>();
        var healthChecks = new List<IHealthCheck>();

        for (int i = 0; i < numberHealthChecks; i++)
        {
            var secondsToSleep = random.Next(2);
            var expectedHealthCheckReport = new Models.HealthCheckResult(
                HealthStatus.Healthy,
                $"Health Check Fixture - {i}"
            );

            secondsToSleepList
                .Add(
                    secondsToSleep
                );

            healthChecks
                .Add(
                    new HealthCheckFixture(
                        $"{healthCheckFixtureNamePrefix} - {i}",
                        expectedHealthCheckReport,
                        secondsToSleep
                    )
                );
        }
        var maxSecondsToSleep = secondsToSleepList.Max();
        var healthCheckService = new Services.HealthCheckService(
            healthChecks
        );

        // Actions
        var healthReport = await healthCheckService
            .CheckHealth();

        // Assertions
        Assert.IsNotNull(healthReport);
        Assert.AreEqual(HealthStatus.Healthy, healthReport.Status);
        Assert.AreEqual("Healthy", healthReport.StatusDescription);
        Assert.IsTrue(healthReport.TotalDuration > TimeSpan.FromSeconds(maxSecondsToSleep));
        Assert.AreEqual(numberHealthChecks, healthReport.Entries.Count);

        for (int i = 0; i < numberHealthChecks; i++)
        {
            var healthCheck = healthReport.Entries[i];

            Assert.AreEqual($"{healthCheckFixtureNamePrefix} - {i}", healthCheck.Name);
            Assert.AreEqual(HealthStatus.Healthy, healthCheck.Status);
            Assert.AreEqual("Healthy", healthCheck.StatusDescription);
            Assert.AreEqual($"Health Check Fixture - {i}", healthCheck.Description);
            Assert.IsTrue(healthCheck.Duration >= TimeSpan.FromSeconds(secondsToSleepList[i]));
        }
    }
}