using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class EstadoRepository(AppDbContext context): EntityRepository<Estado>(context), IEstadoRepository
{

}