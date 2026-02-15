using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientesController(IClienteService clienteService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> Create(ClienteCreateDto dto)
    {
        var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var sucursalId = Guid.Parse(User.FindFirst(c => c.Type == "id_sucursal")?.Value ?? Guid.Empty.ToString());

        var result = await clienteService.CrearClienteAsync(dto, usuarioId, sucursalId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetAll()
    {
        return Ok(await clienteService.ObtenerTodosAsync());
    }
}