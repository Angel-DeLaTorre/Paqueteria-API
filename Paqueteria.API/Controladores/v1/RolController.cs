using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Roles;
using Paqueteria.Application.Modulos.Roles.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class RolController(IRolServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RolResponseDto>>> Get()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }
}