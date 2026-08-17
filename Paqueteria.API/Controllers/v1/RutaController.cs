using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Application.Modulos.Rutas;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class RutaController( IRutaServicio servicio ) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RutaResponseDto>>> GetAll()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{rutaId:guid}")]
    public async Task<ActionResult<RutaResponseDto>> GetById(Guid rutaId)
    {
        var result = await servicio.ObtenerPorIdAsync(rutaId);
        return ProcessResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<RutaResponseDto>> Create([FromBody] RutaCreateDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(RutaUpdateDto dto)
    {
        var result = await servicio.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{rutaId:guid}")]
    public async Task<IActionResult> Delete(Guid rutaId)
    {
        var result = await servicio.EliminarAsync(rutaId);
        return ProcessResult(result);
    }
}