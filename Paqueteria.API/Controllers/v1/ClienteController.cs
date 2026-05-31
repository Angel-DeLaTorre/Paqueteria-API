using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        var result = await service.GetAllAsync(CurrentUser);
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
        var result = await service.CreateAsync(dto, CurrentUser);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] ClienteUpdateDto dto)
    {
        var result = await service.UpdateAsync(dto, CurrentUser);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{sucursalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid sucursalId)
    {
        var result = await service.DeleteAsync(sucursalId, CurrentUser);
        return ProcessResult(result);
    }
}