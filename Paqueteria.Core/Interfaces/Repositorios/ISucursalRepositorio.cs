using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface ISucursalRepositorio
{
    public Task<Sucursal?> ObtenerPorIdAsync(Guid sucursalId, Guid empresaId);
    public Task<IReadOnlyList<Sucursal>> ObtenerTodosAsync(Guid empresaId);
    public Task<Sucursal> AgregarAsync(Sucursal entity);
    public void Eliminar(Sucursal entity);
}