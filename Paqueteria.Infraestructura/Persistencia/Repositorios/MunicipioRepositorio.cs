using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entidades.Catalogos;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class MunicipioRepositorio(AppDbContext context) : EntityRepositorio<Municipio>(context), IMunicipioRepositorio
{
    private readonly AppDbContext _context = context;

    public async Task<IReadOnlyList<Municipio>> ObtenerTodosAsync()
    {
        return await _context.Municipios
            .Include(m => m.Estado)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IEnumerable<Municipio>> ObtenerTodosPorEstadoAsync(string estadoId)
    {
        return await _context.Municipios
            .Where(m => m.EstadoId == estadoId)
            .ToListAsync();
    }
}