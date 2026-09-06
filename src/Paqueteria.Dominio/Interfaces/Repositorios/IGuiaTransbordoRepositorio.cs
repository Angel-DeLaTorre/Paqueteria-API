using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IGuiaTransbordoRepositorio
{
    Task<GuiaTransbordo?> ObtenerPorIdAsync(Guid id);
    Task<IEnumerable<GuiaTransbordo>> ObtenerPorGuiaIdAsync(Guid guiaId);
    Task<IEnumerable<GuiaTransbordo>> ObtenerPorAsignacionIdAsync(Guid asignacionId);
    Task<GuiaTransbordo?> ObtenerUltimoTransbordoActivoAsync(Guid guiaId);
    Task<GuiaTransbordo> AgregarAsync(GuiaTransbordo entity);
}