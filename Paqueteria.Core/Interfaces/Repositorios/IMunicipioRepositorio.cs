using Paqueteria.Core.Entidades.Catalogos;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface IMunicipioRepositorio : IEntityRepositorio<Municipio>
{
    Task<IReadOnlyList<Municipio>> ObtenerTodosAsync();
    Task<IEnumerable<Municipio>> ObtenerTodosPorEstadoAsync(string estadoId);
}