using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class BitacoraAccesoRepository(AppDbContext context) : EntityRepository<BitacoraAcceso>(context) , IBitacoraAccesoRepository
{

}