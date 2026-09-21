using Microsoft.AspNetCore.Mvc;
using Paqueteria.Aplicacion.Modulos.Roles;
using Paqueteria.Comun.Dtos.Roles;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class RolController(IRolServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RolRespuestaDto>>> Get()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }
}