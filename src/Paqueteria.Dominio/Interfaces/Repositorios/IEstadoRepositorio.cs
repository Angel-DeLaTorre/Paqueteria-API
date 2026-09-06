using Paqueteria.Dominio.Entidades.Catalogos;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IEstadoRepositorio
{
    Task<Estado?> ObtenerPorIdAsync(Guid id);
    Task<IReadOnlyList<Estado>> ObtenerTodosAsync();
    Task<Estado> AgregarAsync(Estado entidad);
    void Eliminar(Estado entidad);
}