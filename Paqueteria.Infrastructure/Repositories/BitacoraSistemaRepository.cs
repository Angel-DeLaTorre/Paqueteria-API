using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sistema;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class BitacoraSistemaRepository(AppDbContext context) : EntityRepository<BitacoraSistema>(context) , IBitacoraSistemaRepository
{

}