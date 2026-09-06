using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Modulos.Estados;
using Paqueteria.Application.Modulos.Estados.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class EstadoController(IEstadoServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoRespuestaDto>>> Get()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<EstadoRespuestaDto>> GetById(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }
}