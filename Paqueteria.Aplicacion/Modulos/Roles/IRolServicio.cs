using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Roles;

public interface IRolServicio
{
    Task<Resultado<RolResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Resultado<IEnumerable<RolResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<RolResponseDto>> AgregarAsync(RolCreateDto dto);
    Task<Resultado> ActualizarAsync(RolUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid id);
    Task<Resultado> ActivarAsync(Guid rolId);
    Task<Resultado> DesactivarAsync(Guid rolId);
}