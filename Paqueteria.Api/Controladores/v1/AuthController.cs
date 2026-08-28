using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Modulos.Sesion;
using Paqueteria.Application.Modulos.Sesion.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(ISesionServicio sesionServicio) : PaqueteriaControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<SesionRespuestaDto>> Login(LoginSolicitudDto request)
    {
        var result = await sesionServicio.IniciarSesionAsync(request);
        return ProcessResult(result);
    }
}