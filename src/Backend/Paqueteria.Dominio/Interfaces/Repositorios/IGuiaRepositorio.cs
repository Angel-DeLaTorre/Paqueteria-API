using System.Linq.Expressions;
using Paqueteria.Dominio.Dto;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IGuiaRepositorio
{
    Task<Guia?> ObtenerPorIdAsync(Guid guiaId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Guia>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Guia>> ObtenerFiltroAsync(GuiaFiltroDto filtro, Guid empresaId, bool asTracking = true);
    Task<Guia> AgregarAsync(Guia entity);
    void Eliminar(Guia entity);
    
    Task<IReadOnlyList<TResult>> ObtenerFiltradoYProyectadoAsync<TResult>(
        Expression<Func<Guia, bool>> filtro,
        Expression<Func<Guia, TResult>> proyeccion,
        int pagina,
        int tamanoPagina,
        CancellationToken cancellationToken = default);
}