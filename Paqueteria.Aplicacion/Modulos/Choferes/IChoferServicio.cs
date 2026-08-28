using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Choferes.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Choferes;

public interface IChoferServicio
{
    Task<Respuesta<IReadOnlyList<ChoferResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<ChoferResponseDto>> ObtenerPorIdAsync(Guid choferId);
    Task<Respuesta<ChoferResponseDto>> AgregarAsync(ChoferCreateDto dto);
    Task<Respuesta> ActualizarAsync(ChoferUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid choferId);
    Task<Respuesta> ActivarAsync(Guid choferId);
    Task<Respuesta> DesactivarAsync(Guid choferId);
}