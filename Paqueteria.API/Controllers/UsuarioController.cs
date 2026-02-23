using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController(IUsuarioService service) : PaqueteriaControllerBase
{
    [HttpGet("get")]
    public async Task<ActionResult<List<UsuarioResponseDto>>> GetAll()
    {
        var result = await service.GetAll();
        return ProcessResult(result);
    }

    [HttpGet("{ id:int} ")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(string username)
    {
        var result = await service.GetById(username);
        return ProcessResult(result);
    }


}