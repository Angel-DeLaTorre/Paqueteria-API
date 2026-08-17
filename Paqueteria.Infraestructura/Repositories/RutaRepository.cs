using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class RutaRepository(AppDbContext context) : IRutaRepository
{
    public async Task<Ruta?> ObtenerPorIdAsync(Guid rutaId, Guid empresaId)
    {
        return await context.Rutas
            .Include(r => r.SucursalOrigen)
            .Include(r => r.SucursalDestino)
            .FirstOrDefaultAsync(r => r.Id == rutaId && r.EmpresaId == empresaId);
    }

    public async Task<IEnumerable<Ruta>> ObtenerTodosAsync(Guid empresaId)
    {
        return await context.Rutas
            .Include(r => r.SucursalOrigen)
            .Include(r => r.SucursalDestino)
            .Where(r => r.EmpresaId == empresaId && r.Estatus == EstatusBasico.Activo)
            .ToListAsync();
    }

    public async Task<Ruta> AgregarAsync(Ruta entity)
    {
        var ruta = await context.Rutas.AddAsync(entity);
        return ruta.Entity;
    }

    public void Eliminar(Ruta entity)
    {
        context.Rutas.Remove(entity);
    }
}