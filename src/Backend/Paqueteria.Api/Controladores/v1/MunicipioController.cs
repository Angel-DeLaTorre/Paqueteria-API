using Microsoft.AspNetCore.Mvc;
using Paqueteria.Aplicacion.Modulos.Municipios;
using Paqueteria.Comun.Dtos.Municipios;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class MunicipioController(IMunicipioServicio  servicio) : PaqueteriaControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MunicipioRespuestaDto>>> GetAll()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }
    
    [HttpGet("estado/{estado}")]
    public async Task<ActionResult<IReadOnlyList<MunicipioRespuestaDto>>> GetByEstado([FromRoute] string estado)
    {
        var result = await servicio.ObtenerPorEstadoAsync(estado);
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MunicipioRespuestaDto>> GetById(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }
}