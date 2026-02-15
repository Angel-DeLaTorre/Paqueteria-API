using Paqueteria.Application.DTOs;

namespace Paqueteria.Application.Interfaces.Services;

public interface ISucursalService
{
    Task<SucursalResponseDto?> ObtenerSucursalByIdAsync(Guid sucursalId);
    Task<ICollection<SucursalResponseDto>> ObtenerSucursalesAsync();
    Task<SucursalResponseDto> InsertarSucursalAsync(SucursalCreateDto dto);
    Task<bool> ActualizarSucursalAsync(Guid idSucursal, SucursaUpdateDto dto);
    Task<bool> DesactivarSucursalAsync(Guid sucursalId);
}