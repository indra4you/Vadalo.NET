using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Vadalo.Web.Api.Controllers;

[Route("api/[controller]")]
public sealed class PingController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get(
    ) =>
        base.OkResponse(
            "Pong",
            $"[{DateTimeOffset.UtcNow} UTC] Server is up and running"
        );
}