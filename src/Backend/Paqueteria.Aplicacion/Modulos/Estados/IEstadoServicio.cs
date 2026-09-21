using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Estados;

namespace Paqueteria.Aplicacion.Modulos.Estados;

public interface IEstadoServicio
{
    Task<Respuesta<EstadoRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IEnumerable<EstadoRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<IEnumerable<EstadoRespuestaDto>>> ObtenerPorPais(string pais);
}