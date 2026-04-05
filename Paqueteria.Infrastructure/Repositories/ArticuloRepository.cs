using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities.Sat;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class ArticuloRepository(AppDbContext context) : EntityRepository<Articulo>(context), IArticuloRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<Articulo?> GetByIdAsync(string id) => await _context.Set<Articulo>().FindAsync(id);
}