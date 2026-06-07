using Paqueteria.Core.Entities.Catalogos;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IMunicipioRepository : IEntityRepository<Municipio>
{
    Task<IReadOnlyList<Municipio>> ObtenerMunicipiosAsync();
    Task<IEnumerable<Municipio>> ObtenerMunicipiosPorEstadoAsync(string estadoId);
}