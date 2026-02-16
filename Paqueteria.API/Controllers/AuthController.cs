using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    public async Task<ActionResult<SesionResponseDto>> Login(LoginRequestDto request)
    {
        var response = await authService.LoginAsync(request);

        if (response == null)
            return Unauthorized("Credenciales incorrectos");

        return Ok(response);
    }

}