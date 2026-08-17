using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Application.Modulos.Sesion;
using Paqueteria.Application.Modulos.Sesion.Dtos;

namespace Paqueteria.API.Controllers.v1;

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