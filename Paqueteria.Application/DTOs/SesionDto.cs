using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.DTOs;

public record LoginRequestDto(string Username, string Password);

public record SesionResponseDto(
    string Username,
    string Nombre,
    List<string> Permisos,
    string Token,
    DateTime? Expiracion
)
{
    public static SesionResponseDto FromEntity(Usuario entity, string token, DateTime? expiracion)
    {
        var listaPermisos = entity.UsuarioRoles?
            .Select(ur => ur.Rol)
            .Where(r => r != null && r.RolPermiso != null)
            .SelectMany(r => r.RolPermiso)
            .Where(rp => rp.Permiso != null)
            .Select(rp => rp.Permiso.Nombre)
            .Distinct()
            .ToList() ?? [];
        
        return new SesionResponseDto
        (
            entity.Username,
            entity.Nombre,
            listaPermisos,
            token,
            expiracion 
        );   
    }
};