// Paqueteria.Infrastructure/Repositories/UnitOfWork.cs
using Paqueteria.Application.Interfaces;
using Paqueteria.Infrastructure.Data;
using System.Collections;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private Hashtable? _repositories;

    public IGenericRepository<T> Repository<T>() where T : class
    {
        _repositories ??= new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type]!;
    }

    public async Task<int> Complete(Guid usuarioId, Guid sucursalId)
    {
        var entradas = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .ToList();

        foreach (var entrada in entradas)
        {
            if (entrada.Entity is BitacoraSistema) continue;

            var nombreTabla = entrada.Entity.GetType().Name;
            var accion = entrada.State switch
            {
                EntityState.Added => AccionBitacora.Insert,
                EntityState.Modified => AccionBitacora.Update,
                EntityState.Deleted => AccionBitacora.Delete,
                _ => AccionBitacora.Lecture
            };

            var bitacora = new BitacoraSistema
            {
                IdUsuario = usuarioId,
                IdSucursal = sucursalId,
                Accion = accion,
                ValoresAnteriores = JsonSerializer.Serialize(entrada.Entity),
                IpCliente = "Local-Sync",
                FechaEvento = DateTime.UtcNow
            };

            await context.Set<BitacoraSistema>().AddAsync(bitacora);
        }

        return await context.SaveChangesAsync();
    }

    public void Dispose() => context.Dispose();
}