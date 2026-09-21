using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Municipios;

namespace Paqueteria.Aplicacion.Modulos.Municipios;

public interface IMunicipioServicio
{
    Task<Respuesta<IReadOnlyList<MunicipioRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<MunicipioRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IReadOnlyList<MunicipioRespuestaDto>>> ObtenerPorEstadoAsync(string estado);
}