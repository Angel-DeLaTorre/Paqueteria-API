using Microsoft.AspNetCore.Mvc;
using Paqueteria.Core.Common;

namespace Paqueteria.API.Controllers;

[ApiController]
public abstract class PaqueteriaControllerBase : ControllerBase
{
    protected ActionResult<T> ProcessResult<T>(Resultado<T> result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    protected IActionResult ProcessResult(Resultado result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}