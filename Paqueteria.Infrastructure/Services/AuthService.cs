using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;
using Paqueteria.Core.Settings;
using Paqueteria.Infrastructure.Repositories;

namespace Paqueteria.Infrastructure.Services;

public class AuthService(IUsuarioRepository repoUsuario, IOptions<JwtSettings> jwtOptions) : IAuthService
{
    public async Task<Result<SesionResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var usuario = await repoUsuario.GetByUsernameAsync(request.Username);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
            return Result<SesionResponseDto>.Failure(CodigoRespuesta.NotFound, $"Usuario o contraseña incorrectos.");


        if (usuario.Estatus != EstatusGenerico.Activo)
            return Result<SesionResponseDto>.Failure(CodigoRespuesta.Forbidden, $"Usuario bloqueado");

        return Result<SesionResponseDto>.Success( GenerateAuthResponse(usuario) );
    }

    private SesionResponseDto GenerateAuthResponse(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Value.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                //new Claim("sucursalId", usuario.SucursalId.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.DurationInMinutes),
            Issuer = jwtOptions.Value.Issuer,
            Audience = jwtOptions.Value.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return SesionResponseDto.FromEntity(usuario, token, tokenDescriptor.Expires);
    }
}