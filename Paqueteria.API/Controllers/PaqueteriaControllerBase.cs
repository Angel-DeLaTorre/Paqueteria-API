using Microsoft.AspNetCore.Mvc;
using Paqueteria.Core.Common;

namespace Paqueteria.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class PaqueteriaControllerBase : ControllerBase
{
    protected ActionResult<T> ProcessResult<T>(Result<T> result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    protected IActionResult ProcessResult(Result result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}