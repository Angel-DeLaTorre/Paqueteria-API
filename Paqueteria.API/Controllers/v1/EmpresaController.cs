using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Application.Modulos.Empresas;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class EmpresaController(IEmpresaServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmpresaResponseDto>>> Get()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<EmpresaResponseDto>> Get(Guid id)
    {
        var result = await servicio.ObtenerPorIdAsync();
        return ProcessResult(result);
    }


    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<EmpresaResponseDto>> Create([FromBody] EmpresaCreateDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] EmpresaUpdateDto dto)
    {
        var result = await servicio.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{empresaId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid empresaId)
    {
        var result = await servicio.EliminarAsync(empresaId);
        return ProcessResult(result);
    }
}