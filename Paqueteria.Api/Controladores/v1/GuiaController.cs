using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Modulos.Guias.Dtos;
using Paqueteria.Application.Modulos.Guias.Interfaces;
using Paqueteria.Core.Dto;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class GuiaController
(
    IGuiaServicio service,
    IGuiaPdfServicio pdfServicio
) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GuiaRespuestaDto>>> Get()
    {
        var result = await service.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GuiaRespuestaDto>> Get(Guid id)
    {
        var result = await service.ObtenerPorIdAsync(id);
        return ProcessResult(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GuiaCreadaDto>> Create([FromBody] GuiaCrearDto dto)
    {
        var result = await service.AgregarAsync(dto);
        return ProcessResult(result);
    }
    
    [HttpPost("filtro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IReadOnlyList<GuiaRespuestaDto>>> ObtenerFiltro([FromBody] GuiaFiltroDto request)
    {
        var result = await service.ObtenerFiltroAsync(request);
        return ProcessResult(result);
    }
    
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] GuiaActualizarDto dto)
    {
        var result = await service.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await service.EliminarAsync(id);
        return ProcessResult(result);
    }
    
    [HttpGet("etiqueta/{id:guid}")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, "application/pdf")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GeneraEtiqueta(Guid id)
    {
        var pdfBytes = await pdfServicio.GenerarEtiquetaPaqueteAsync(id);
        return File(
            fileContents: pdfBytes.Datos ?? throw new InvalidOperationException(), 
            contentType: "application/pdf", 
            fileDownloadName: $"Etiqueta_{id}.pdf"
        );
    }
    
    [HttpGet("remision-pdf/{id:guid}")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, "application/pdf")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GenerarRemisionPdf(Guid id)
    {
        var pdfBytes = await pdfServicio.GenerarRemisionPdfAsync(id);
        return File(
            fileContents: pdfBytes.Datos ?? throw new InvalidOperationException(), 
            contentType: "application/pdf", 
            fileDownloadName: $"Etiqueta_{id}.pdf"
        );
    }
    
    
}