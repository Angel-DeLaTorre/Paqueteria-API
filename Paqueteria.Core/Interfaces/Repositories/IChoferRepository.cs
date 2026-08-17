using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IChoferRepository
{
    Task<Chofer?> ObtenerPorIdAsync(Guid choferId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Chofer>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true);
    Task<Chofer> AgregarAsync(Chofer entity);
    void Eliminar(Chofer entity);
}