using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class PaqueteriaControllerBase : ControllerBase
{
    protected Guid UsuarioId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                                           ?? throw new UnauthorizedAccessException("Usuario no identificado"));

    protected Guid SucursalId => Guid.Parse(User.FindFirst("sucursalId")?.Value
                                            ?? ""); //throw new UnauthorizedAccessException("Sucursal no identificada"));

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