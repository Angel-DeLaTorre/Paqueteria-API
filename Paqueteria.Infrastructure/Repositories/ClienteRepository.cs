using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : IClienteRepository
{
    public async Task<Cliente?> GetByIdAsync(Guid clienteId, Guid empresaId, bool asTracking = true)
    {
       IQueryable<Cliente> query = context.Clientes;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(s => s.Id == clienteId && s.EmpresaId == empresaId);
    }
    
    public async Task<DireccionCliente?> GetDireccionByIdAsync(Guid clienteId, Guid direccionId, bool asTracking = true)
    {
        IQueryable<DireccionCliente> query = context.DireccionClientes;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(dc => 
            dc.Id == direccionId 
            && dc.ClienteId == clienteId);
    }

    public async Task<IReadOnlyList<Cliente>> GetAllAsync(Guid empresaId)
    {
            return await context.Clientes
            .Include( x => x.Direcciones )
                .ThenInclude( d => d.Direccion)
                    .ThenInclude( dir=> dir.Municipio )
            .Where(c => c.EmpresaId == empresaId)
            .AsNoTracking() 
            .ToListAsync();
    }

    public async Task<Cliente> AddAsync(Cliente entity)
    {
        var ruta = await context.Clientes.AddAsync(entity);
        return ruta.Entity;
    }

    public async void Delete(Cliente entity)
    {
        context.Clientes.Remove(entity);
    }

    public async Task AddDireccion(DireccionCliente dir)
    {
        await context.DireccionClientes.AddAsync(dir);
    }
}