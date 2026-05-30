using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class EmpresaRepository(AppDbContext context) : EntityRepository<Empresa>(context), IEmpresaRepository
{

}