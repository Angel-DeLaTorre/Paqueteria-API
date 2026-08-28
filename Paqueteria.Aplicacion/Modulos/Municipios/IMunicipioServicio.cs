using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Municipios.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Municipios;

public interface IMunicipioServicio
{
    Task<Respuesta<IReadOnlyList<MunicipioResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<MunicipioResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IReadOnlyList<MunicipioResponseDto>>> ObtenerPorEstadoAsync(string estado);
}