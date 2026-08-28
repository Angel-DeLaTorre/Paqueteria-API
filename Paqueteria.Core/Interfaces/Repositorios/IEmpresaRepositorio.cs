using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface IEmpresaRepositorio
{
    Task<Empresa?> ObtenerPorIdAsync(Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Empresa>> ObtenerTodosAsync(bool asTracking = true);
    Task<Empresa> AgregarAsync(Empresa entity);
    void Eliminar(Empresa entity);
}