using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Application.Modulos.Articulos;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ArticuloController(IArticuloServicio service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ArticuloResponseDto>>> Get()
    {
        var result = await service.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticuloResponseDto>> Get(Guid id)
    {
        var result = await service.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }
    
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ArticuloResponseDto>> Create(ArticuloCreateDto dto)
    {
        var result = await service.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] ArticuloUpdateDto dto)
    {
        var result = await service.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{articuloId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid articuloId)
    {
        var result = await service.EliminarAsync(articuloId);
        return ProcessResult(result);
    }
}