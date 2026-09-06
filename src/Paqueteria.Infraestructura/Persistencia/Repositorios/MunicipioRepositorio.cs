using Microsoft.EntityFrameworkCore;
using Paqueteria.Dominio.Entidades.Catalogos;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class MunicipioRepositorio(AppDbContext context) : IMunicipioRepositorio
{
    public async Task<IReadOnlyList<Municipio>> ObtenerTodosAsync()
    {
        return await context.Municipios
            .Include(m => m.Estado)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IEnumerable<Municipio>> ObtenerTodosPorEstadoAsync(string estadoId)
    {
        return await context.Municipios
            .Where(m => m.EstadoId == estadoId)
            .ToListAsync();
    }
    
    public async Task<Municipio?> ObtenerPorIdAsync(Guid municipioId) => await context.Municipios.FindAsync(municipioId);
}