    using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Application.Modulos.Roles;

namespace Paqueteria.API.Controllers.v1;

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