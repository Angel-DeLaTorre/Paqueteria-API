using Microsoft.AspNetCore.Mvc;
using Paqueteria.Aplicacion.Modulos.Estados;
using Paqueteria.Comun.Dtos.Estados;

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
    
    [HttpGet("{pais}")]
    public async Task<ActionResult<IEnumerable<EstadoRespuestaDto>>> ObtenerPorPais([FromRoute] string pais)
    {
        var result = await servicio.ObtenerPorPais(pais);
        return ProcessResult(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<EstadoRespuestaDto>> GetById(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }
}