using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class AsignacionRepository(AppDbContext context) : EntityRepository<Asignacion>(context) , IAsignacionRepository
{

}