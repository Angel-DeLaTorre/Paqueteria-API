using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Modulos.Rutas;
using Paqueteria.Application.Modulos.Rutas.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class RutaController( IRutaServicio servicio ) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RutaRespuestaDto>>> GetAll()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{rutaId:guid}")]
    public async Task<ActionResult<RutaRespuestaDto>> GetById(Guid rutaId)
    {
        var result = await servicio.ObtenerPorIdAsync(rutaId);
        return ProcessResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<RutaRespuestaDto>> Create([FromBody] RutaCrearDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(RutaActualizarDto dto)
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