using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Infrastructure.Services;

namespace Paqueteria.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SucursalController(SucursalService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<SucursalResponseDto>>> Get()
    {
        var sucursales = await service.ObtenerSucursalesAsync();
        return Ok(sucursales);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SucursalResponseDto>> Get(Guid id)
    {
        var sucursal = await service.ObtenerSucursalByIdAsync(id);

        if (sucursal == null)
            return NotFound(new { mensaje = $"La sucursal con ID {id} no existe." });

        return Ok(sucursal);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] SucursaUpdateDto dto)
    {
        var result = await service.ActualizarSucursalAsync(id, dto);

        if (!result)
        {
            return NotFound(new { mensaje = $"No se pudo encontrar la sucursal con ID {id} para actualizar." });
        }

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await service.DesactivarSucursalAsync(id);

        if (!result)
        {
            return NotFound(new { mensaje = $"No se pudo encontrar la sucursal con ID {id} para desactivar." });
        }

        return NoContent();
    }
}