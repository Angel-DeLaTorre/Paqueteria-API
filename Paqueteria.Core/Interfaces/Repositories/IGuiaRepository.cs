using Paqueteria.Core.Dto;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IGuiaRepository
{
    Task<Guia?> ObtenerPorIdAsync(Guid guiaId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Guia>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Guia>> ObtenerFiltroAsync(GuiaFiltroDto filtro, Guid empresaId, bool asTracking = true);
    Task<Guia> AgregarAsync(Guia entity);
    void Eliminar(Guia entity);
}