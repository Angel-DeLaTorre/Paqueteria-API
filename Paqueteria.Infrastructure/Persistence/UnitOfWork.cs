using Microsoft.EntityFrameworkCore.Storage;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _currentTransaction;

    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _currentTransaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await context.SaveChangesAsync();
            if (_currentTransaction != null) await _currentTransaction.CommitAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }
}