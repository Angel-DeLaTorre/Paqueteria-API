using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController(IUsuarioService service) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UsuarioResponseDto>>> GetAll()
    {
        var result = await service.GetAllAsync();
        return ProcessResult(result);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(string username)
    {
        var result = await service.GetByUsername(username);
        return ProcessResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> Create([FromBody] UsuarioCreateDto dto)
    {
        var result = await service.CreateAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UsuarioUpdateDto dto)
    {
        var result = await service.UpdateAsync(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{usuarioId:guid}")]
    public async Task<IActionResult> Delete(Guid usuarioId)
    {
        var result = await service.DeleteAsync(usuarioId);
        return ProcessResult(result);
    }
}