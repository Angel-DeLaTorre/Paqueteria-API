using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;
using Paqueteria.Core.Settings;

namespace Paqueteria.Infrastructure.Services;

public class AuthService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtOptions) : IAuthService
{
    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var usuarios = await unitOfWork.Repository<Usuario>()
            .GetAsync(u => u.Username == request.Username && u.Estatus == EstatusGenerico.Activo);

        var usuario = usuarios.FirstOrDefault();

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
        {
            return null;
        }

        return GenerateAuthResponse(usuario);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private AuthResponse GenerateAuthResponse(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Value.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
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

        return new AuthResponse(
            usuario.IdUsuario,
            usuario.Username,
            usuario.Nombre,
            usuario.Rol.ToString(),
            tokenHandler.WriteToken(token),
            tokenDescriptor.Expires.Value
        );
    }
}