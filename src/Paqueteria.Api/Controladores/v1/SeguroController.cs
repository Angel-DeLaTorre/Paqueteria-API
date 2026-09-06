using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Modulos.Seguros;
using Paqueteria.Application.Modulos.Seguros.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class SeguroController(ISeguroServicio servicio ) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SeguroRespuestaDto>>> Get()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SeguroRespuestaDto>> Get(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }


    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SeguroRespuestaDto>> Create(SeguroCrearDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] SeguroActualizarDto dto)
    {
        var result = await servicio.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{seguroId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid seguroId)
    {
        var result = await servicio.EliminarAsync(seguroId);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("desactivar/{seguroId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desactivar(Guid seguroId)
    {
        var result = await servicio.DesactivarAsync(seguroId);
        return ProcessResult(result);
    }
}