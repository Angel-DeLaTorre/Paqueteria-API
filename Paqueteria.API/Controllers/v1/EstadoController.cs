using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class EstadoController(IEstadoService service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoResponseDto>>> Get()
    {
        var result = await service.GetEstadosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<EstadoResponseDto>> GetById(Guid id)
    {
        var result = await service.GetEstadoByIdAsync(id);
        return ProcessResult(result);
    }
}