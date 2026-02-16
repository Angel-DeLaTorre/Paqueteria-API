using Paqueteria.Core.Entities;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface IUsuarioRepository : IEntityRepository<Usuario>
{
    Task<Usuario?> GetByUsernameAsync(string username);
}