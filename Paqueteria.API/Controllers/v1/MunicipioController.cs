using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class MunicipioController(IMunicipioService  service) : PaqueteriaControllerBase
{
    [HttpGet("estado/{estado}")]
    public async Task<ActionResult<IReadOnlyList<MunicipioResponseDto>>> Get(string estado)
    {
        var result = await service.ObtenerMunicipiosPorEstadoAsync(estado);
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MunicipioResponseDto>> Get(Guid id)
    {
        var result = await service.ObtenerMunicipioAsync(id);
        return ProcessResult(result);
    }
}