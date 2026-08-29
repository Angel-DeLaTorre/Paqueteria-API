using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Entidades.Sistema;

namespace Paqueteria.Application.Modulos.Sesion.Dtos;

public record LoginRequestDto(
    [param: Required] string Username, 
    [param: Required] string Password
    );

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