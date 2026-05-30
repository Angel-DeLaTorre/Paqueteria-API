using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class BitacoraSistemaRepository(AppDbContext context) : EntityRepository<BitacoraSistema>(context) , IBitacoraSistemaRepository
{

}