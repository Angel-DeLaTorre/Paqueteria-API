using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController(IUsuarioService service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UsuarioResponseDto>>> GetAll()
    {
        var result = await service.GetAll();
        return ProcessResult(result);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(string username)
    {
        var result = await service.GetById(username);
        return ProcessResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> Create(UsuarioCreateDto dto)
    {
        var result = await service.Create(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UsuarioUpdateDto dto)
    {
        var result = await service.Update(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid usuarioId)
    {
        var result = await service.Delete(usuarioId);
        return ProcessResult(result);
    }
}