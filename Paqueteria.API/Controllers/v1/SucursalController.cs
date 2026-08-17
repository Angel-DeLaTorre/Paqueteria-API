using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Application.Modulos.Sucursales;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class SucursalController(ISucursalServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SucursalResponseDto>>> Get()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SucursalResponseDto>> Get(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SucursalResponseDto>> Create(SucursalCreateDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] SucursaUpdateDto dto)
    {
        var result = await servicio.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{sucursalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid sucursalId)
    {
        var result = await servicio.EliminarAsync(sucursalId);
        return ProcessResult(result);
    }
}