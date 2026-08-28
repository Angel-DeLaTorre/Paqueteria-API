using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Municipios;
using Paqueteria.Application.Modulos.Municipios.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class MunicipioController(IMunicipioServicio  servicio) : PaqueteriaControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MunicipioResponseDto>>> GetAll()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }
    
    [HttpGet("{estado}")]
    public async Task<ActionResult<IReadOnlyList<MunicipioResponseDto>>> GetByEstado(string estado)
    {
        var result = await servicio.ObtenerPorEstadoAsync(estado);
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MunicipioResponseDto>> GetById(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }
}