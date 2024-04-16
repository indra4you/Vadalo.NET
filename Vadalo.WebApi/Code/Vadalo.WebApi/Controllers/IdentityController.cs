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
            .InviteByEmailAddress(
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
}