using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : EntityRepository<Cliente>(context), IClienteRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IReadOnlyList<Cliente>> GetAllAsync(Guid empresaId)
    {
            return await _context.Clientes
            .Include( x => x.Direcciones )
                .ThenInclude( d => d.Direccion)
                    .ThenInclude( dir=> dir.Municipio )
            .Where(c => c.EmpresaId == empresaId)
            .AsNoTracking() 
            .ToListAsync();
    }
    public async Task AddDireccion(DireccionCliente dir)
    {
        await _context.DireccionClientes.AddAsync(dir);
    }
}