using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface IUsuarioRepository : IEntityRepository<Usuario>
{
    Task<Usuario?> GetByUsernameAsync(string username);
}