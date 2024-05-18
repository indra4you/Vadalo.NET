using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Vadalo.Web.Api.Controllers;

[Route("api/v1/[controller]")]
public sealed class IdentityController : ControllerBase
{
    [HttpPatch]
    [AllowAnonymous]
    public async Task<IActionResult> Patch(
        [FromBody] IdentityInviteRequest identityInviteRequest,
        [FromServices] Identity.IdentityService identityService
    )
    {
        identityInviteRequest
            .ValidateAndThrow();

        await identityService
            .Invite(
                new(
                    "admin@vadalo.com", // TODO: Get "InvitedByEmailAddress" from JWT Token
                    identityInviteRequest.InviteeEmailAddress!
                )
            );

        return base
            .OkResponse(
                "Hello!"
            );
    }

    [HttpPost("send-otp")]
    [AllowAnonymous]
    public async Task<IActionResult> SendOneTimePassword(
        [FromBody] Identity.SendOneTimePasswordRequest sendOneTimePasswordRequest,
        [FromServices] Identity.IdentityService identityService
    )
    {
        await identityService
            .SendOneTimePassword(
                sendOneTimePasswordRequest
            );

        return base
            .OkResponse(
                "One time password sent successfully"
            );
    }

    [HttpPost("validate-otp")]
    [AllowAnonymous]
    public async Task<IActionResult> ValidateOneTimePassword(
        [FromBody] Identity.ValidateOneTimePasswordRequest validateOneTimePasswordRequest,
        [FromServices] Identity.IdentityService identityService
    )
    {
        var isOneTimePasswordValid = await identityService
            .ValidateOneTimePassword(
                validateOneTimePasswordRequest
            );

        return isOneTimePasswordValid
            ? base
                .OkResponse(
                    "One time password validated successfully"
                )
            : base
                .Unauthorized();
    }
}