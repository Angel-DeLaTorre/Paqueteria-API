using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface ISeguroRepositorio
{
    Task<Seguro?> ObtenerPorIdAsync(Guid seguroId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Seguro>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true);
    Task<Seguro> AgregarAsync(Seguro entity);
    void Eliminar(Seguro entity);
}