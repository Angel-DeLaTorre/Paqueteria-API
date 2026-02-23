using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IAuthService
{
    Task<Result<SesionResponseDto>> LoginAsync(LoginRequestDto request);
}