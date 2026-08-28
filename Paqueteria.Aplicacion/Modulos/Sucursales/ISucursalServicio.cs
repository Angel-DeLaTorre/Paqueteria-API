using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Sucursales.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Sucursales;

public interface ISucursalServicio
{
    Task<Respuesta<IReadOnlyList<SucursalResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<SucursalResponseDto>> ObtenerPorIdAsync(Guid sucursalId);
    Task<Respuesta<SucursalResponseDto>> AgregarAsync(SucursalCreateDto dto);
    Task<Respuesta> ActualizarAsync(SucursaUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid sucursalId);
}