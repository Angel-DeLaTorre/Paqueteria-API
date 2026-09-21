using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Permisos;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IPermisoServicio
{
    Task<Respuesta<List<PermisoRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<PermisoRespuestaDto>> ObtenerPorIdAsync(Guid permisoId);
    Task<Respuesta<PermisoRespuestaDto>> CrearAsync(PermisoCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid permisoId, PermisoActualizarDto dto);
    Task<Respuesta> ActivarAsync(Guid permisoId);
    Task<Respuesta> DesactivarAsync(Guid permisoId);
    Task<Respuesta> EliminarAsync(Guid permisoId);
}