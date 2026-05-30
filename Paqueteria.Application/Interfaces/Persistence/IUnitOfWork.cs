using Paqueteria.Application.Interfaces.Repositories;

namespace Paqueteria.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IUsuarioRepository Usuarios { get; }
    IRolRepository Roles { get; }
    IPermisoRepository Permisos { get; }
    
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}