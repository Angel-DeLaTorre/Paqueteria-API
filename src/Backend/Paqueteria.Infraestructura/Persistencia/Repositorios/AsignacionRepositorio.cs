using Microsoft.EntityFrameworkCore;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class AsignacionRepositorio(AppDbContext context) :  IAsignacionRepositorio
{
    public async Task<Asignacion?> ObtenerPorIdAsync(Guid asignacionId, Guid empresaId)
    {
        return await context.Asignaciones.FirstOrDefaultAsync(a => a.Id == asignacionId && a.EmpresaId == empresaId);
    }

    public async Task<IEnumerable<Asignacion>> ObtenerTodosAsync(Guid empresaId)
    {
        return await context.Asignaciones.Where(a => a.EmpresaId == empresaId)
            .Include(a => a.SucursalOrigen)
            .Include(a => a.SucursalDestino)
            .Include(a => a.Chofer)
            .ToListAsync();
    }

    public async Task<Asignacion> AgregarAsync(Asignacion entity)
    {
        var newEntity = await context.Asignaciones.AddAsync(entity);
        return newEntity.Entity;
    }

    public void Eliminar(Asignacion entity)
    {
        context.Asignaciones.Remove(entity);
    }
    
    public async Task<IEnumerable<Asignacion>> ObtenerParaReporteSalidasAsync(
        Guid empresaId, 
        Guid? sucursalOrigenId, 
        DateTime fechaInicio, 
        DateTime fechaFin)
    {
        var query = context.Asignaciones
            .AsNoTracking()
            .Include(a => a.SucursalOrigen)
            .Include(a => a.SucursalDestino)
            .Include(a => a.Chofer)
            .Include(a => a.Guias)
            .ThenInclude(g => g.ClienteOrigen)
            .Include(a => a.Guias)
            .ThenInclude(g => g.ClienteDestino)
            .Include(a => a.Guias)
            .ThenInclude(g => g.SucursalDestino)
            .Include(a => a.Guias)
            .ThenInclude(g => g.ArticulosGuia)
            .Where(a => a.EmpresaId == empresaId 
                        && a.FechaCreacion >= fechaInicio 
                        && a.FechaCreacion <= fechaFin);

        if (sucursalOrigenId.HasValue && sucursalOrigenId.Value != Guid.Empty)
        {
            query = query.Where(a => a.SucursalOrigenId == sucursalOrigenId.Value);
        }

        return await query
            .OrderBy(a => a.FechaCreacion)
            .ToListAsync();
    }
}