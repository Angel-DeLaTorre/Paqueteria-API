using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface ISeguroRepository
{
    Task<Seguro?> ObtenerPorIdAsync(Guid seguroId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Seguro>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true);
    Task<Seguro> AgregarAsync(Seguro entity);
    void Eliminar(Seguro entity);
}