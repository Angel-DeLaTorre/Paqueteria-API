using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class MunicipioRepository(AppDbContext context) : EntityRepository<Municipio>(context), IMunicipioRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Municipio>> ObtenerMunicipiosPorEstadoAsync(string estadoId)
    {
        return await _context.Municipios
            .Where(m => m.EstadoId == estadoId)
            .ToListAsync();
    }
}