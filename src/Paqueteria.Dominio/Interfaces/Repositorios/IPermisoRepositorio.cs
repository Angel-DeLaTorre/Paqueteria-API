using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IPermisoRepositorio
{
    Task<Permiso?> ObtenerPorIdAsync(Guid id, Guid empresaId);
    Task<Permiso?> ObtenerPorNombreAsync(string nombre, Guid empresaId);
    Task<IEnumerable<Permiso>> ObtenerTodosAsync(Guid empresaId);
    Task AgregarAsync(Permiso permission);
    void Eliminar(Permiso permission);
    
    Task<IReadOnlyList<Permiso>> ObtenerPorIdsYEmpresaAsync(IEnumerable<Guid> permisosIds, Guid empresaId);

}