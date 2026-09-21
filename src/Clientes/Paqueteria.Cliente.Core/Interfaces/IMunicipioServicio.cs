using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Municipios;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IMunicipioServicio
{
    Task<Respuesta<List<MunicipioRespuestaDto>>> ObtenerTodosAsync(string estado);
}