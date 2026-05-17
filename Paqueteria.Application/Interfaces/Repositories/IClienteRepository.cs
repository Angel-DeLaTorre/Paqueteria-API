using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface IClienteRepository : IEntityRepository<Cliente>
{
    Task AddDireccion(DireccionCliente dir);
    Task<IReadOnlyList<Cliente>> GetAllAsync(Guid empresaId);
}