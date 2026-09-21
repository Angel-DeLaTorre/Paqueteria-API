using Microsoft.AspNetCore.Mvc;
using Paqueteria.Aplicacion.Modulos.Sesion;
using Paqueteria.Comun.Dtos.Sesion;

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