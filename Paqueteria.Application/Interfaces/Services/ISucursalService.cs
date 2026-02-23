using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface ISucursalService
{
    Task<Result<SucursalResponseDto>> ObtenerSucursalByIdAsync(Guid sucursalId);
    Task<Result<ICollection<SucursalResponseDto>>> ObtenerSucursalesAsync();
    Task<Result<SucursalResponseDto>> InsertarSucursalAsync(SucursalCreateDto dto);
    Task<Result> ActualizarSucursalAsync(Guid idSucursal, SucursaUpdateDto dto);
    Task<Result> DesactivarSucursalAsync(Guid sucursalId);
}