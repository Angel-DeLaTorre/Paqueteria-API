using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class MunicipioRepository(AppDbContext context) : EntityRepository<Municipio>(context), IMunicipioRepository
{
    public Task<IEnumerable<Municipio>> ObtenerMunicipiosPorEstadoAsync(string estadoId)
    {
        throw new NotImplementedException();
    }
}