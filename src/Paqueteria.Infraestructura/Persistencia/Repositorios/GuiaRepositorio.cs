using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Paqueteria.Dominio.Dto;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class GuiaRepositorio(AppDbContext context) : IGuiaRepositorio
{
    public async Task<Guia?> ObtenerPorIdAsync(Guid guiaId, Guid empresaId, bool asTracking = true)
    {
        IQueryable<Guia> query = context.Guias;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .Include(g => g.DireccionOrigen)
            .ThenInclude(d => d.Direccion)
            .ThenInclude(m => m.Municipio)
            .Include(g => g.DireccionDestino)
            .ThenInclude(d => d.Direccion)
            .ThenInclude(m => m.Municipio)
            .Include(g => g.SucursalOrigen)
            .Include(g => g.SucursalDestino)
            .Include(g => g.UsuarioCobro)
            .Include(g => g.ClienteOrigen)
            .Include(g => g.ClienteDestino)
            .Include(g => g.ArticulosGuia)
            .FirstOrDefaultAsync(g => g.Id == guiaId && g.EmpresaId == empresaId);
    }
    
    public async Task<IReadOnlyList<Guia>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Guia> query = context.Guias;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .Where(c => c.EmpresaId == empresaId)
            .Include(d => d.DireccionOrigen)
                .ThenInclude(m => m.Direccion)
                    .ThenInclude(d => d.Municipio)
            .Include(d => d.DireccionDestino)
                .ThenInclude(m => m.Direccion)
                    .ThenInclude(d => d.Municipio)
            .Include(g => g.SucursalDestino)
            .Include(g => g.SucursalOrigen)
            .Include(g => g.UsuarioCobro)
            .Include(g => g.ClienteOrigen)
            .Include(g => g.ClienteDestino)
            .Include(g => g.ArticulosGuia)
            .ToListAsync();
    }
    
    public async Task<Guia> AgregarAsync(Guia entity)
    {
        var ruta = await context.Guias.AddAsync(entity);
        return ruta.Entity;
    }

    public void Eliminar(Guia entity)
    {
        context.Guias.Remove(entity);
    }

    #region Query

    public async Task<IReadOnlyList<Guia>> ObtenerFiltroAsync(GuiaFiltroDto filtro, Guid empresaId, bool asTracking = true)
    {
        var query = context.Guias.AsNoTracking();
        
        query = query.Where(g => g.EmpresaId == empresaId);
        
        if (!string.IsNullOrWhiteSpace(filtro.Clave))
        {
            query = query.Where(g => g.Clave.Contains(filtro.Clave));
        }

        if (filtro.Estatus.HasValue)
        {
            query = query.Where(g => g.Estatus == filtro.Estatus.Value);
        }

        if (filtro.SucursalOrigenId.HasValue)
        {
            query = query.Where(g => g.SucursalOrigenId == filtro.SucursalOrigenId.Value);
        }
        
        if (filtro.EstaAsignado.HasValue)
        {
            
            query = filtro.EstaAsignado.Value ? 
                query.Where(g => g.AsignacionId != null)
                :
                query.Where(g => g.AsignacionId == null);
        }

        
        return await query
            .Where(c => c.EmpresaId == empresaId)
            .Include(g => g.ClienteOrigen)
            .Include(g => g.ClienteDestino)
            .Include(d => d.DireccionOrigen)
            .ThenInclude(m => m.Direccion)
            .ThenInclude(d => d.Municipio)
            .Include(d => d.DireccionDestino)
            .ThenInclude(m => m.Direccion)
            .ThenInclude(d => d.Municipio)
            .Include(g => g.SucursalDestino)
            .Include(g => g.SucursalOrigen)
            .Include(g => g.UsuarioCobro)
            .ToListAsync();
    }
    
    public async Task<IReadOnlyList<TResult>> ObtenerFiltradoYProyectadoAsync<TResult>(
        Expression<Func<Guia, bool>> filtro,
        Expression<Func<Guia, TResult>> proyeccion,
        int pagina,
        int tamanoPagina,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<Guia>()
            .AsNoTracking()
            .Where(filtro)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .Select(proyeccion)
            .ToListAsync(cancellationToken);
    }

    #endregion
}