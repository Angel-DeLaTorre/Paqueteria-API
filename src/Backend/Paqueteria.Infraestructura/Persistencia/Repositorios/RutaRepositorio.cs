using Microsoft.EntityFrameworkCore;
using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class RutaRepositorio(AppDbContext context) : IRutaRepositorio
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