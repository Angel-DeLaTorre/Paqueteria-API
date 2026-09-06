using Paqueteria.Application.Modulos.Roles.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Roles;

public interface IRolServicio
{
    Task<Respuesta<RolRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IEnumerable<RolRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<RolRespuestaDto>> AgregarAsync(RolCrearDto dto);
    Task<Respuesta> ActualizarAsync(RolActualizarDto dto);
    Task<Respuesta> AgregarPermisosAsync(RolAgregarPermisoDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
    Task<Respuesta> ActivarAsync(Guid rolId);
    Task<Respuesta> DesactivarAsync(Guid rolId);
}