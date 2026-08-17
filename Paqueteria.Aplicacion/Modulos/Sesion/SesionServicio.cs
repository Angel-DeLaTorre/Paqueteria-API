using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Sesion.Configuracion;
using Paqueteria.Application.Modulos.Sesion.Dtos;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Entities.Sistema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Sesion;

public class SesionServicio(
    IUnitOfWork unidadDeTrabajo, 
    IOptions<JwtOpciones> jwtOpciones,
    IHashServicio hashServicio) : ISesionServicio
{
    public async Task<Resultado<SesionRespuestaDto>> IniciarSesionAsync(LoginSolicitudDto solicitud)
    {
        var usuario = await unidadDeTrabajo.Usuarios.ObtenerPorUsernameAsync(solicitud.Username, true);

        if (usuario == null)
            return Resultado<SesionRespuestaDto>.Error(CodigosError.Users.NotFound);

        if (!hashServicio.Verificar(solicitud.Password, usuario.Password))
            return Resultado<SesionRespuestaDto>.Error(CodigosError.Users.ContrasennaErronea);

        if (usuario.Estatus != EstatusBasico.Activo)
            return Resultado<SesionRespuestaDto>.Error(CodigosError.Users.Bloqueado);
        
        usuario.RegistrarAcceso();
        await unidadDeTrabajo.CompletarAsync();

        return Resultado<SesionRespuestaDto>.Exitoso(GenerarRespuestaAutenticacion(usuario));
    }

    public Task<Resultado> CambiarContraseniaAsync(LoginSolicitudDto solicitud)
    {
        throw new NotImplementedException();
    }

    private SesionRespuestaDto GenerarRespuestaAutenticacion(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOpciones.Value.Key);
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new (ClaimTypes.Name, usuario.Username),
            new ("empresa_id", usuario.EmpresaId.ToString())
        };
        
        var permisos = usuario.UsuarioRoles?
            .Select(ur => ur.Rol)
            .Where(r => r != null && r.RolPermiso != null)
            .SelectMany(r => r.RolPermiso!)
            .Where(rp => rp.Permiso != null)
            .Select(rp => rp.Permiso!.Nombre)
            .Distinct()
            .ToList() ?? [];

        claims.AddRange(permisos.Select(permiso => new Claim("permisos", permiso)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtOpciones.Value.DurationInMinutes),
            Issuer = jwtOpciones.Value.Issuer,
            Audience = jwtOpciones.Value.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var jwtString = tokenHandler.WriteToken(securityToken);
        
        return new SesionRespuestaDto(
            usuario.Username,
            usuario.Nombre,
            permisos,
            jwtString,
            tokenDescriptor.Expires
        );
    }
}