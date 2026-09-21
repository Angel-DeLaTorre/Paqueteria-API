using Microsoft.AspNetCore.Mvc;
using Paqueteria.Aplicacion.Modulos.Permisos;
using Paqueteria.Comun.Dtos.Permisos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class PermisoControlador(IPermisoServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PermisoRespuestaDto>>> ObtenerTodos()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }
    
    [HttpGet("{permisoId:guid}")]
    public async Task<ActionResult<PermisoRespuestaDto>> ObtenerPorId(Guid permisoId)
    {
        var result = await servicio.ObtenerPorIdAsync(permisoId);
        return ProcessResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<PermisoRespuestaDto>> Crear([FromBody] PermisoCrearDto permiso)
    {
        var resultado = await servicio.AgregarAsync(permiso);
        return ProcessResult(resultado);
    }

    [HttpPatch("{permisoId:guid}")]
    public async Task<ActionResult> Actualizar([FromBody] PermisoActualizarDto permiso, Guid permisoId)
    {
        var resultado = await servicio.ActualizarAsync(permisoId, permiso);
        return ProcessResult(resultado);
    }

    [HttpDelete("{permisoId:guid}")]
    public async Task<ActionResult> Eliminar(Guid permisoId)
    {
        var resultado = await servicio.EliminarAsync(permisoId);
        return ProcessResult(resultado);
    }

    [HttpPatch("{permisoId:guid}/activar")]
    public async Task<ActionResult> Activar(Guid permisoId)
    {
        var resultado = await servicio.ActivarAsync(permisoId);
        return ProcessResult(resultado);
    }
    
    [HttpPatch("{permisoId:guid}/desactivar")]
    public async Task<ActionResult> Desactivar(Guid permisoId)
    {
        var resultado = await servicio.DesactivarAsync(permisoId);
        return ProcessResult(resultado);
    }
}