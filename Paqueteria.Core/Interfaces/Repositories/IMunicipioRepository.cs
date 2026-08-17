using Paqueteria.Core.Entities.Catalogos;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IMunicipioRepository : IEntityRepository<Municipio>
{
    Task<IReadOnlyList<Municipio>> ObtenerTodosAsync();
    Task<IEnumerable<Municipio>> ObtenerTodosPorEstadoAsync(string estadoId);
}