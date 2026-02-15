using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class MunicipioRepository(AppDbContext context) : IMunicipioRepository
{
    public async Task<Municipio?> ObtenerMunicipioAsync(Guid id)
    {
        return await context.Municipios
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.IdMunicipio == id);
    }

    public async Task<IEnumerable<Municipio>> ObtenerMunicipiosPorEstadoAsync(string estadoId)
    {
        return await context.Municipios
            .AsNoTracking()
            .Where(m => m.IdEstado == estadoId)
            .OrderBy(m => m.Nombre)
            .ToListAsync();
    }
}