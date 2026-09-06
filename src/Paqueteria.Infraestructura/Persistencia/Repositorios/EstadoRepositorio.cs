using Microsoft.EntityFrameworkCore;
using Paqueteria.Dominio.Entidades.Catalogos;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class EstadoRepositorio(AppDbContext context): IEstadoRepositorio
{
    public async Task<Estado?> ObtenerPorIdAsync(Guid id) => await context.Estados.FindAsync(id);

    public async Task<IReadOnlyList<Estado>> ObtenerTodosAsync() => await context.Estados.ToListAsync();

    public async Task<Estado> AgregarAsync(Estado entidad)
    {
        var estado = await context.Estados.AddAsync(entidad);
        return estado.Entity;
    }

    public void Eliminar(Estado entidad) => context.Estados.Remove(entidad);
}