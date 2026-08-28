using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Permisos.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Permisos;

public interface IPermisoServicio
{
    Task<Respuesta<PermisoResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IEnumerable<PermisoResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<PermisoResponseDto>> AgregarAsync(PermisoCreateDto dto);
    Task<Respuesta> ActualizarAsync(PermisoUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
    Task<Respuesta> ActivarAsync(Guid permisoId);
    Task<Respuesta> DesactivarAsync(Guid permisoId);
}