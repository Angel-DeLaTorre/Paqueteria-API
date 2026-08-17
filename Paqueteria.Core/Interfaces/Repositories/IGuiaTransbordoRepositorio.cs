using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IGuiaTransbordoRepositorio
{
    Task<GuiaTransbordo?> ObtenerPorIdAsync(Guid id);
    Task<IEnumerable<GuiaTransbordo>> ObtenerPorGuiaIdAsync(Guid guiaId);
    Task<IEnumerable<GuiaTransbordo>> ObtenerPorAsignacionIdAsync(Guid asignacionId);
    Task<GuiaTransbordo?> ObtenerUltimoTransbordoActivoAsync(Guid guiaId);
    Task<GuiaTransbordo> AgregarAsync(GuiaTransbordo entity);
}