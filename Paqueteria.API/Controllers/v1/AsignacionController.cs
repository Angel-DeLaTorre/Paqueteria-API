using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Application.Modulos.Asignaciones;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AsignacionController(IAsignacionServicio service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AsignacionResponseDto>>> Get()
    {
        var result = await service.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{asignacionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AsignacionResponseDto>> Get(Guid asignacionId)
    {
        var result = await service.ObtenerPorIdAsync(asignacionId);
        return ProcessResult(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AsignacionResponseDto>> Create(AsignacionCreateDto dto)
    {
        var result = await service.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] AsignacionUpdateDto dto)
    {
        var result = await service.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{asignacionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid asignacionId)
    {
        var result = await service.EliminarAsync(asignacionId);
        return ProcessResult(result);
    }

    #region Reportes

    [HttpGet("reporte-salidas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReporteSalidas(
        [FromQuery] Guid? sucursalOrigenId,
        [FromQuery] DateTime? fechaInicio,
        [FromQuery] DateTime? fechaFin)
    {
        var result = await service.GenerarReporteSalidasPdfAsync(sucursalOrigenId, fechaInicio, fechaFin);
        
        if (!result.IsSuccess)
            return ProcessResult(result).Result;
        
        var nombreArchivo = sucursalOrigenId.HasValue
            ? $"Reporte_Salidas_{sucursalOrigenId.Value}_{DateTime.Now:yyyyMMdd}.pdf"
            : $"Reporte_Salidas_Todas_{DateTime.Now:yyyyMMdd}.pdf";

        return File(
            fileContents: result.Value, 
            contentType: "application/pdf", 
            fileDownloadName: nombreArchivo);
        
    }

    #endregion
}