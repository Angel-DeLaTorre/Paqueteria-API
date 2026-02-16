using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public sealed class SucursalRepository(AppDbContext context) : EntityRepository<Sucursal>(context), ISucursalRepository
{

}