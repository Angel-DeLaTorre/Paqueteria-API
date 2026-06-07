using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.DTOs;

public record UsuarioCreateDto(
    string Nombre,
    string Username,
    string Password,
    List<Guid> Roles
)
{
    public Usuario ToEntity(string hashedPassword, Guid empresaId) => Usuario.Create(Nombre, Username, hashedPassword, empresaId);
};

public record UsuarioUpdateDto(
    Guid UsuarioId,
    string Nombre
)
{
    public void UpdateEntity(Usuario entity)
    {
        entity.Nombre = Nombre;
    }
};

public record UsuarioResponseDto(
    Guid Id,
    string Nombre,
    string Username,
    List<RolResponseDto> Roles,
    DateTime? FechaUltimoAcesso
)
{
    public static UsuarioResponseDto FromEntity(Usuario entity)
    {
        var roles = new List<RolResponseDto>();
        
        return new UsuarioResponseDto(
            entity.Id,
            entity.Nombre,
            entity.Username,
            roles,
            entity.FechaUltimoAcceso
        );
    }
};