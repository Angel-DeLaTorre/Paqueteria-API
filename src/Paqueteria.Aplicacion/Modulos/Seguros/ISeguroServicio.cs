using Paqueteria.Application.Modulos.Seguros.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Seguros;

public interface ISeguroServicio
{
    Task<Respuesta<IReadOnlyList<SeguroRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<SeguroRespuestaDto>> ObtenerPorIdAsync(Guid seguroId);
    Task<Respuesta<SeguroRespuestaDto>> AgregarAsync(SeguroCrearDto dto);
    Task<Respuesta> ActualizarAsync(SeguroActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid seguroId);
    Task<Respuesta> DesactivarAsync(Guid seguroId);
}