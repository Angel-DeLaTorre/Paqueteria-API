using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class UsuarioRepository(AppDbContext context) : EntityRepository<Usuario>(context), IUsuarioRepository
{
    public async Task<Usuario?> GetByUsernameAsync(string username)
    {
        var usuario = await context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == username);

        return usuario;
    }
}