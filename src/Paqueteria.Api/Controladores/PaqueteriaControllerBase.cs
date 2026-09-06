using Microsoft.AspNetCore.Mvc;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;

namespace Paqueteria.API.Controladores;

[ApiController]
public abstract class PaqueteriaControllerBase : ControllerBase
{
    protected ActionResult<T> ProcessResult<T>(Respuesta<T> result)
    {
        return result.EsExitoso ? Ok(result) : BadRequest(result);
    }
    
    protected ActionResult ProcessResult(Respuesta result)
    {
        if (result.EsExitoso)
        {
            // Si es un comando POST exitoso que creó un recurso, podrías diferenciarlo, 
            // por defecto Ok(result) o NoContent() para actualizaciones/eliminaciones.
            return Ok(result);
        }

        return MapErrorToResponse(result);
    }

    private ActionResult MapErrorToResponse(Respuesta resultado)
    {
        // Evalúa el tipo de error según las convenciones de tu arquitectura
        return resultado.DetalleError?.Code switch
        {
            CodigosError.TipoError.NoEncontrado => NotFound(resultado),
            CodigosError.TipoError.Duplicado => Conflict(resultado),
            CodigosError.TipoError.NoAutorizado => Unauthorized(resultado),
            CodigosError.TipoError.Prohibido => Forbid(),
            _ => BadRequest(resultado)
        };
    }
}