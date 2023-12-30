using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Vadalo.Web.Api.Controllers;

[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(
        [FromServices] HealthCheck.IHealthCheckService healthCheckService,
        CancellationToken cancellationToken = default
    )
    {
        var healthReport = await healthCheckService
            .CheckHealth(
                cancellationToken
            );

        return base
            .OkResponse(
                healthReport,
                $"Health Check Status is '{healthReport.StatusDescription}'"
            );
    }
}