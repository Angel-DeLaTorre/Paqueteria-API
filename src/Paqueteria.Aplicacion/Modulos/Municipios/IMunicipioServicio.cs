using Paqueteria.Application.Modulos.Municipios.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Municipios;

public interface IMunicipioServicio
{
    Task<Respuesta<IReadOnlyList<MunicipioRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<MunicipioRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IReadOnlyList<MunicipioRespuestaDto>>> ObtenerPorEstadoAsync(string estado);
}