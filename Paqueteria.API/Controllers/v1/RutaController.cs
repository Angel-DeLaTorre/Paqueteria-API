using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class RutaController( IRutaService service ) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RutaResponseDto>>> GetAll()
    {
        var result = await service.GetAllAsync();
        return ProcessResult(result);
    }

    [HttpGet("{rutaId:guid}")]
    public async Task<ActionResult<RutaResponseDto>> GetById(Guid rutaId)
    {
        var result = await service.GetByIdAsync(rutaId);
        return ProcessResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<RutaResponseDto>> Create([FromBody] RutaCreateDto dto)
    {
        var result = await service.CreateAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(RutaUpdateDto dto)
    {
        var result = await service.UpdateAsync(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{rutaId:guid}")]
    public async Task<IActionResult> Delete(Guid rutaId)
    {
        var result = await service.DeleteAsync(rutaId);
        return ProcessResult(result);
    }
}