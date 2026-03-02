using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class PaqueteriaControllerBase : ControllerBase
{
    protected UserContext CurrentUser => new UserContext(
        UserId: Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty),
        Username: User.FindFirstValue(ClaimTypes.Name),
        Sucursal: Guid.Parse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty),
        IpAddress: HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
    );

    protected ActionResult<T> ProcessResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Value is null ? NoContent() : Ok(result.Value);
        }

        return new ActionResult<T>(ProcessError(result.Error!));
    }

    protected IActionResult ProcessResult(Result result)
    {
        return result.IsSuccess ? NoContent() : ProcessError(result.Error!);
    }

    private ActionResult ProcessError(BaseError error)
    {
        return error.Code switch
        {
            CodigoRespuesta.NotFound => NotFound(error.Description),
            CodigoRespuesta.BadRequest => BadRequest(error.Description),
            CodigoRespuesta.Unauthorized => Unauthorized(error.Description),
            CodigoRespuesta.Forbidden => Forbid(),
            CodigoRespuesta.Conflict => Conflict(error.Description),
            CodigoRespuesta.Failure => StatusCode(500, error.Description),
            _ => BadRequest(error.Code)
        };
    }


}