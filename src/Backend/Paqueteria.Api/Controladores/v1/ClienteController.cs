using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Aplicacion.Modulos.Clientes;
using Paqueteria.Comun.Dtos.Clientes;
using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.API.Controladores.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class ClienteController(IClienteServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClienteRespuestaDto>>> ObtenerTodos()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ClienteRespuestaDto>> ObtenerPorId(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ClienteRespuestaDto>> Agregar([FromBody] ClienteCrearDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar([FromBody] ClienteActualizarDto dto)
    {
        var result = await servicio.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{sucursalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(Guid sucursalId)
    {
        var result = await servicio.EliminarAsync(sucursalId);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("{id:guid}/activar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activar([FromRoute] Guid id)
    {
        var result = await servicio.ActivarAsync(id);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("{id:guid}/desactivar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desativar([FromRoute] Guid id)
    {
        var result = await servicio.DesactivarAsync(id);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPost("{clienteId:guid}/direccion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AgregarDireccion([FromRoute] Guid clienteId, [FromBody] DireccionDto dto)
    {
        var result = await servicio.AgregarDireccionAsync(clienteId, dto);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("{clienteId:guid}/direccion/{direccionId:guid}/activar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActivarDireccion([FromRoute] Guid direccionId, Guid clienteId)
    {
        var result = await servicio.ActivarDireccionAsync(clienteId, direccionId);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("{clienteId:guid}/direccion/{direccionId:guid}/desactivar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DesativarDireccion([FromRoute] Guid direccionId, Guid clienteId)
    {
        var result = await servicio.DesactivarDireccionAsync(clienteId, direccionId);
        return ProcessResult(result);
    }
    
    
}