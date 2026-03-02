using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Infrastructure.Services;

namespace Paqueteria.API.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class SucursalController(SucursalService service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<SucursalResponseDto>>> Get()
    {
        var result = await service.ObtenerSucursalesAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SucursalResponseDto>> Get(Guid id)
    {
        var result = await service.ObtenerSucursalByIdAsync(id);
        return ProcessResult(result);
    }


    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SucursalResponseDto>> Create(SucursalCreateDto dto)
    {
        var result = await service.InsertarSucursalAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] SucursaUpdateDto dto)
    {
        var result = await service.ActualizarSucursalAsync(id, dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await service.DesactivarSucursalAsync(id);
        return ProcessResult(result);
    }
}