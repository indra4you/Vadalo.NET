using Microsoft.AspNetCore.Mvc;

namespace Vadalo.Web.Api.Controllers;

public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected IActionResult OkResponse<TData>(
        TData data,
        string message = "",
        string status = ViewModel.ActionStatus.Successful
    ) =>
        base
            .Ok(
                new ViewModel.ActionResponse<TData>(
                    status,
                    message,
                    data
                )
            );
}