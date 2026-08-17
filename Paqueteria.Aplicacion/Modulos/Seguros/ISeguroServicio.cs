using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Seguros;

public interface ISeguroServicio
{
    Task<Resultado<IReadOnlyList<SeguroResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<SeguroResponseDto>> ObtenerPorIdAsync(Guid seguroId);
    Task<Resultado<SeguroResponseDto>> AgregarAsync(SeguroCreateDto dto);
    Task<Resultado> ActualizarAsync(SeguroUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid seguroId);
    Task<Resultado> DesactivarAsync(Guid seguroId);
}