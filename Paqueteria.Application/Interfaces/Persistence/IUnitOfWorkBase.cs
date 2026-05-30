namespace Paqueteria.Application.Interfaces.Persistence;

public interface IUnitOfWorkBase : IDisposable
{
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}