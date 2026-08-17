using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Choferes;

public interface IChoferServicio
{
    Task<Resultado<IReadOnlyList<ChoferResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<ChoferResponseDto>> ObtenerPorIdAsync(Guid choferId);
    Task<Resultado<ChoferResponseDto>> AgregarAsync(ChoferCreateDto dto);
    Task<Resultado> ActualizarAsync(ChoferUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid choferId);
    Task<Resultado> ActivarAsync(Guid choferId);
    Task<Resultado> DesactivarAsync(Guid choferId);
}