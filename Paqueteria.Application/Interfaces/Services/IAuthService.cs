using Paqueteria.Application.DTOs;

namespace Paqueteria.Application.Interfaces.Services;

public interface IAuthService
{
    Task<SesionResponseDto?> LoginAsync(LoginRequestDto request);
}