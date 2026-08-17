using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Municipios;

public interface IMunicipioServicio
{
    Task<Resultado<IReadOnlyList<MunicipioResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<MunicipioResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Resultado<IReadOnlyList<MunicipioResponseDto>>> ObtenerPorEstadoAsync(string estado);
}