using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Roles.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Roles;

public interface IRolServicio
{
    Task<Respuesta<RolResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IEnumerable<RolResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<RolResponseDto>> AgregarAsync(RolCreateDto dto);
    Task<Respuesta> ActualizarAsync(RolUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
    Task<Respuesta> ActivarAsync(Guid rolId);
    Task<Respuesta> DesactivarAsync(Guid rolId);
}