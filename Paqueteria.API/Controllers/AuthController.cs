using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces;

namespace Paqueteria.API.Controllers;

public class AuthController(IAuthService authService) : ControllerBase
{

    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        if (response == null)
            return Unauthorized("Credenciales incorrectos");

        return Ok(response);
    }

}