using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClienteController(IClienteService clienteService) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetAll()
    {
        var result = await clienteService.GetAllAsync();
        //ProcessResult<IEnumerable<ClienteResponseDto>>(result);
        return Ok(await clienteService.GetAllAsync());

    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClienteResponseDto>> GetById(System.Guid id)
    {
        return  Ok(await clienteService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> Create(ClienteCreateDto dto)
    {
        var result = await clienteService.CreateAsync(dto, UsuarioId, SucursalId);
        return Ok(result);
    }
}