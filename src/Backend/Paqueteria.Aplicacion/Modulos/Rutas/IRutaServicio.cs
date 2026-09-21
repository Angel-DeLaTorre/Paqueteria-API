using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Rutas;

namespace Paqueteria.Aplicacion.Modulos.Rutas;

public interface IRutaServicio
{
    Task<Respuesta<IReadOnlyList<RutaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<RutaRespuestaDto>> ObtenerPorIdAsync(Guid rutaId);
    Task<Respuesta<RutaRespuestaDto>> AgregarAsync(RutaCrearDto dto);
    Task<Respuesta> ActualizarAsync(RutaActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid rutaId);
}