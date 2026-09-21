using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Estados;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IEstadoServicio
{
    Task<Respuesta<List<EstadoRespuestaDto>>> ObtenerTodosAsync(string pais);
}