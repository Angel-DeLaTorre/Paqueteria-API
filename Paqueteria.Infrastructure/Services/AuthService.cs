using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sistema;
using Paqueteria.Core.Enums;
using Paqueteria.Core.Settings;

namespace Paqueteria.Infrastructure.Services;

public class AuthService(IUnitOfWork unit, IOptions<JwtSettings> jwtOptions) : IAuthService
{
    public async Task<Result<SesionResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var usuario = await unit.Usuarios.GetByUsernameAsync(request.Username, true);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
            return Result<SesionResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);


        if (usuario.Estatus != EstatusBasico.Activo)
            return Result<SesionResponseDto>.Failure(ErrorCodes.Users.Bloqueado);
        
        await unit.Usuarios.RegistrarIngreso(usuario);

        await unit.CompleteAsync();

        return Result<SesionResponseDto>.Success( GenerateAuthResponse(usuario) );
    }

    public Task<Result> CambiarPasswordAsync(LoginRequestDto request)
    {
        throw new NotImplementedException();
    }

    private SesionResponseDto GenerateAuthResponse(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Value.Key);
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new (ClaimTypes.Name, usuario.Username),
            new ("empresa_id", usuario.EmpresaId.ToString())
        };
        
        var permisos = usuario.UsuarioRoles?
            .Select(ur => ur.Rol)
            .Where(r => r != null && r.RolPermiso != null)
            .SelectMany(r => r.RolPermiso)
            .Where(rp => rp.Permiso != null)
            .Select(rp => rp.Permiso.Nombre) // Extrae "clientes.modificar", "clientes.consultar", etc.
            .Distinct() // Evitar duplicados
            .ToList() ?? new List<string>();

        claims.AddRange(permisos.Select(permiso => new Claim("permisos", permiso)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.DurationInMinutes),
            Issuer = jwtOptions.Value.Issuer,
            Audience = jwtOptions.Value.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        
        var jwtString = tokenHandler.WriteToken(securityToken);
        
        return SesionResponseDto.FromEntity(usuario, jwtString, tokenDescriptor.Expires);
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}