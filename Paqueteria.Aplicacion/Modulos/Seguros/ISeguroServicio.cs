using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Seguros.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Seguros;

public interface ISeguroServicio
{
    Task<Respuesta<IReadOnlyList<SeguroResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<SeguroResponseDto>> ObtenerPorIdAsync(Guid seguroId);
    Task<Respuesta<SeguroResponseDto>> AgregarAsync(SeguroCreateDto dto);
    Task<Respuesta> ActualizarAsync(SeguroUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid seguroId);
    Task<Respuesta> DesactivarAsync(Guid seguroId);
}