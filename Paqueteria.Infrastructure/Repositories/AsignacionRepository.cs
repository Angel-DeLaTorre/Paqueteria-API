using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class AsignacionRepository(AppDbContext context) :  IAsignacionRepository
{
    public async Task<Asignacion?> GetByIdAsync(Guid asignacionId, Guid empresaId)
    {
        return await context.Asignaciones.FirstOrDefaultAsync(a => a.Id == asignacionId && a.EmpresaId == empresaId);
    }

    public async Task<IEnumerable<Asignacion>> GetAllAsync(Guid empresaId)
    {
        return await context.Asignaciones.Where(a => a.EmpresaId == empresaId).ToListAsync();
    }

    public async Task<Asignacion> AddAsync(Asignacion entity)
    {
        var newEntity = await context.Asignaciones.AddAsync(entity);
        return newEntity.Entity;
    }

    public void Delete(Asignacion entity)
    {
        context.Asignaciones.Remove(entity);
    }
}