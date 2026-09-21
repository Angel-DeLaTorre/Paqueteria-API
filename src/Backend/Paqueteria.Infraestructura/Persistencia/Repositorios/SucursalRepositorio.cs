using Microsoft.EntityFrameworkCore;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class SucursalRepositorio(AppDbContext context) :  ISucursalRepositorio
{
    public async Task<Sucursal?> ObtenerPorIdAsync(Guid sucursalId, Guid empresaId)
    {
        return await context.Sucursales
            .FirstOrDefaultAsync(s => s.Id == sucursalId && s.EmpresaId == empresaId);
    }

    public async Task<IReadOnlyList<Sucursal>> ObtenerTodosAsync(Guid empresaId)
    {
        return await context.Sucursales
            .Where(r => r.EmpresaId == empresaId)
            .Include(s => s.Direccion)
            .Include(s => s.Direccion.Municipio)
            .ToListAsync();
    }

    public async Task<Sucursal> AgregarAsync(Sucursal entity)
    {
        var ruta = await context.Sucursales.AddAsync(entity);
        return ruta.Entity;
    }

    public void Eliminar(Sucursal entity)
    {
        context.Sucursales.Remove(entity);
    }
}