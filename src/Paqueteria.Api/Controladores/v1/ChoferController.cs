using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Modulos.Choferes;
using Paqueteria.Application.Modulos.Choferes.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ChoferController(IChoferServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ChoferRespuestaDto>>> Get()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{choferId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChoferRespuestaDto>> Get(Guid choferId)
    {
        var result = await servicio.ObtenerPorIdAsync(choferId);
        return ProcessResult(result);
    }


    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChoferRespuestaDto>> Create(ChoferCrearDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] ChoferActualizarDto dto)
    {
        var result = await servicio.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{choferId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid choferId)
    {
        var result = await servicio.EliminarAsync(choferId);
        return ProcessResult(result);
    }
}