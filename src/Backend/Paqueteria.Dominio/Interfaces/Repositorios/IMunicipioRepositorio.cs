using Paqueteria.Dominio.Entidades.Catalogos;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IMunicipioRepositorio
{
    Task<IReadOnlyList<Municipio>> ObtenerTodosAsync();
    Task<IEnumerable<Municipio>> ObtenerTodosPorEstadoAsync(string estadoId);
    Task<Municipio?> ObtenerPorIdAsync(Guid municipioId);
}