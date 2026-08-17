using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IRutaRepository
{
    public Task<Ruta?> ObtenerPorIdAsync(Guid rutaId, Guid empresaId);
    public Task<IEnumerable<Ruta>> ObtenerTodosAsync(Guid empresaId);
    public Task<Ruta> AgregarAsync(Ruta entity);
    public void Eliminar(Ruta entity);
}