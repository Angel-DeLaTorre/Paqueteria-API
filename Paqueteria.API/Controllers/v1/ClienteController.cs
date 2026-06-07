using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class ClienteController(IClienteService service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClienteResponseDto>>> Get()
    {
        var result = await service.GetAllAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ClienteResponseDto>> Get(Guid id)
    {
        var result = await service.GetByIdAsync(id);
        return ProcessResult(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ClienteResponseDto>> Create([FromBody] ClienteCreateDto dto)
    {
        var result = await service.CreateAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] ClienteUpdateDto dto)
    {
        var result = await service.UpdateAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{sucursalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid sucursalId)
    {
        var result = await service.DeleteAsync(sucursalId);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("{id:guid}/activar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activar([FromRoute] Guid id)
    {
        var result = await service.ActivarAsync(id);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("{id:guid}/desactivar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desativar([FromRoute] Guid id)
    {
        var result = await service.DesactivarAsync(id);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("clientes/{clienteId:guid}/direcciones/{direccionId:guid}/activar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActivarDireccion([FromRoute] Guid direccionId, Guid clienteId)
    {
        var result = await service.ActivarDireccionAsync(clienteId, direccionId);
        return ProcessResult(result);
    }
    
    [Authorize]
    [HttpPut("clientes/{clienteId:guid}/direcciones/{direccionId:guid}/desactivar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DesativarDireccion([FromRoute] Guid direccionId, Guid clienteId)
    {
        var result = await service.DesactivarDireccionAsync(clienteId, direccionId);
        return ProcessResult(result);
    }
    
    
}