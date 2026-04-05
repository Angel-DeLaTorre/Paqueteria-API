using Paqueteria.Core.Entities.Catalogos;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface IMunicipioRepository : IEntityRepository<Municipio>
{
    Task<IEnumerable<Municipio>> ObtenerMunicipiosPorEstadoAsync(string estadoId);
}