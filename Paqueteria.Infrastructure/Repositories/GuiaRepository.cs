using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class GuiaRepository(AppDbContext context) : EntityRepository<Guia>(context), IGuiaRepository
{

}