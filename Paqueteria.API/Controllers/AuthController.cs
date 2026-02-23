using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAuthService authService) : PaqueteriaControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<SesionResponseDto>> Login(LoginRequestDto request)
    {
        var result = await authService.LoginAsync(request);
        return ProcessResult(result);
    }
}