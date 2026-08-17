using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Sucursales;

public interface ISucursalServicio
{
    Task<Resultado<IReadOnlyList<SucursalResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<SucursalResponseDto>> ObtenerPorIdAsync(Guid sucursalId);
    Task<Resultado<SucursalResponseDto>> AgregarAsync(SucursalCreateDto dto);
    Task<Resultado> ActualizarAsync(SucursaUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid sucursalId);
}