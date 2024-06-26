﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Vadalo.Web.Api.Controllers;

[Route("api/v1/[controller]")]
public sealed class PingController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get(
    ) =>
        base
            .OkResponse(
                "Pong",
                $"[{DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm:ss} UTC] Server is up and running"
            );
}