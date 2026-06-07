using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class GuiaController(IGuiaService service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GuiaResponseDto>>> Get()
    {
        var result = await service.GetAllAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GuiaResponseDto>> Get(Guid id)
    {
        var result = await service.GetByIdAsync(id);
        return ProcessResult(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GuiaResponseDto>> Create([FromBody] GuiaCreateDto dto)
    {
        var result = await service.CreateAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] GuiaUpdateDto dto)
    {
        var result = await service.UpdateAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{guiaId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid guiaId)
    {
        var result = await service.DeleteAsync(guiaId);
        return ProcessResult(result);
    }
}