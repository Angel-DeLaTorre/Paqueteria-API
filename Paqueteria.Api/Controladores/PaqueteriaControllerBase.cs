using Microsoft.AspNetCore.Mvc;
using Paqueteria.Core.Comun;

namespace Paqueteria.API.Controladores;

[ApiController]
public abstract class PaqueteriaControllerBase : ControllerBase
{
    protected ActionResult<T> ProcessResult<T>(Respuesta<T> result)
    {
        return result.EsExitoso ? Ok(result) : BadRequest(result);
    }

    protected IActionResult ProcessResult(Respuesta result)
    {
        return result.EsExitoso ? Ok(result) : BadRequest(result);
    }
}