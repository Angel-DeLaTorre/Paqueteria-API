using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Rutas;

public interface IRutaServicio
{
    Task<Resultado<IReadOnlyList<RutaResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<RutaResponseDto>> ObtenerPorIdAsync(Guid rutaId);
    Task<Resultado<RutaResponseDto>> AgregarAsync(RutaCreateDto dto);
    Task<Resultado> ActualizarAsync(RutaUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid rutaId);
}