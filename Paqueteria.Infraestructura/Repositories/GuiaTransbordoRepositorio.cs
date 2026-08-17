using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class GuiaTransbordoRepositorio(AppDbContext context) : IGuiaTransbordoRepositorio
{
    public async Task<GuiaTransbordo?> ObtenerPorIdAsync(Guid id)
    {
        return await context.Set<GuiaTransbordo>()
            .Include(gt => gt.Guia)
            .Include(gt => gt.Asignacion)
            .Include(gt => gt.SucursalTransbordo)
            .FirstOrDefaultAsync(gt => gt.Id == id);
    }

    public async Task<IEnumerable<GuiaTransbordo>> ObtenerPorGuiaIdAsync(Guid guiaId)
    {
        return await context.Set<GuiaTransbordo>()
            .AsNoTracking()
            .Include(gt => gt.Asignacion)
            .Include(gt => gt.SucursalTransbordo)
            .Where(gt => gt.GuiaId == guiaId)
            .OrderByDescending(gt => gt.FechaEscaneoIngreso)
            .ToListAsync();
    }

    public async Task<IEnumerable<GuiaTransbordo>> ObtenerPorAsignacionIdAsync(Guid asignacionId)
    {
        return await context.Set<GuiaTransbordo>()
            .AsNoTracking()
            .Include(gt => gt.Guia)
            .Include(gt => gt.SucursalTransbordo)
            .Where(gt => gt.AsignacionId == asignacionId)
            .OrderBy(gt => gt.FechaEscaneoIngreso)
            .ToListAsync();
    }

    public async Task<GuiaTransbordo?> ObtenerUltimoTransbordoActivoAsync(Guid guiaId)
    {
        return await context.Set<GuiaTransbordo>()
            .Where(gt => gt.GuiaId == guiaId && gt.FechaEscaneoSalida == null)
            .OrderByDescending(gt => gt.FechaEscaneoIngreso)
            .FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<GuiaTransbordoResponseDto>> ObtenerHistorialProyectadoAsync(
        Guid guiaId, 
        IGuiaTransbordoMapeador mapeador)
    {
        return await context.Set<GuiaTransbordo>()
            .AsNoTracking()
            .Where(gt => gt.GuiaId == guiaId)
            .OrderByDescending(gt => gt.FechaEscaneoIngreso)
            .Select(mapeador.ProyeccionRespuesta) // Proyección directa en SQL
            .ToListAsync();
    }

    public async Task<GuiaTransbordo> AgregarAsync(GuiaTransbordo entity)
    {
        var entry = await context.Set<GuiaTransbordo>().AddAsync(entity);
        return entry.Entity;
    }
}