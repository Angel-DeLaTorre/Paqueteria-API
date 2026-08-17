using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IEmpresaRepository
{
    Task<Empresa?> ObtenerPorIdAsync(Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Empresa>> ObtenerTodosAsync(bool asTracking = true);
    Task<Empresa> AgregarAsync(Empresa entity);
    void Eliminar(Empresa entity);
}