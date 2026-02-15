using Paqueteria.Core.Entities;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface IMunicipioRepository
{
    Task<Municipio?> ObtenerMunicipioAsync(Guid id);
    Task<IEnumerable<Municipio>> ObtenerMunicipiosPorEstadoAsync(string estadoId);
}