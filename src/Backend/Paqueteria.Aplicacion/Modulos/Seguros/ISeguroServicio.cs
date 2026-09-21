using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Seguros;

namespace Paqueteria.Aplicacion.Modulos.Seguros;

public interface ISeguroServicio
{
    Task<Respuesta<IReadOnlyList<SeguroRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<SeguroRespuestaDto>> ObtenerPorIdAsync(Guid seguroId);
    Task<Respuesta<SeguroRespuestaDto>> AgregarAsync(SeguroCrearDto dto);
    Task<Respuesta> ActualizarAsync(SeguroActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid seguroId);
    Task<Respuesta> DesactivarAsync(Guid seguroId);
}