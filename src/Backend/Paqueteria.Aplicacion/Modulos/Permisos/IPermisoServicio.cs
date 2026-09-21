using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Permisos;

namespace Paqueteria.Aplicacion.Modulos.Permisos;

public interface IPermisoServicio
{
    Task<Respuesta<PermisoRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IEnumerable<PermisoRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<PermisoRespuestaDto>> AgregarAsync(PermisoCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid permisoId, PermisoActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
    Task<Respuesta> ActivarAsync(Guid permisoId);
    Task<Respuesta> DesactivarAsync(Guid permisoId);
}