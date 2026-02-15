using Paqueteria.Application.DTOs;

namespace Paqueteria.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    string HashPassword(string password);
}