using Paqueteria.Application.Modulos.Rutas.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Rutas;

public interface IRutaServicio
{
    Task<Respuesta<IReadOnlyList<RutaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<RutaRespuestaDto>> ObtenerPorIdAsync(Guid rutaId);
    Task<Respuesta<RutaRespuestaDto>> AgregarAsync(RutaCrearDto dto);
    Task<Respuesta> ActualizarAsync(RutaActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid rutaId);
}