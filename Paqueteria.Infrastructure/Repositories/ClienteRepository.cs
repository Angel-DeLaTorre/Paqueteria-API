using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class ClienteRepository(AppDbContext context) : EntityRepository<Cliente>(context), IClienteRepository
{

}