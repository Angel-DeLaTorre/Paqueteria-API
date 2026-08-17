using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Permisos;

public interface IPermisoServicio
{
    Task<Resultado<PermisoResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Resultado<IEnumerable<PermisoResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<PermisoResponseDto>> AgregarAsync(PermisoCreateDto dto);
    Task<Resultado> ActualizarAsync(PermisoUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid id);
    Task<Resultado> ActivarAsync(Guid permisoId);
    Task<Resultado> DesactivarAsync(Guid permisoId);
}