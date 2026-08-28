using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface IRutaRepositorio
{
    public Task<Ruta?> ObtenerPorIdAsync(Guid rutaId, Guid empresaId);
    public Task<IEnumerable<Ruta>> ObtenerTodosAsync(Guid empresaId);
    public Task<Ruta> AgregarAsync(Ruta entity);
    public void Eliminar(Ruta entity);
}