using Paqueteria.Core.Entidades.Sat;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface IArticuloRepositorio
{
    Task<Articulo?> ObtenerPorIdAsync(Guid articuloId, bool asTracking = true);
    Task<IReadOnlyList<Articulo>> ObtenerTodosAsync(bool asTracking = true);
    Task<Articulo> AgregarAsync(Articulo entity);
    void Eliminar(Articulo entity);
}