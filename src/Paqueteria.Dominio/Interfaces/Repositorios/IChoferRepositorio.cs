using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IChoferRepositorio
{
    Task<Chofer?> ObtenerPorIdAsync(Guid choferId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Chofer>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true);
    Task<Chofer> AgregarAsync(Chofer entity);
    void Eliminar(Chofer entity);
}