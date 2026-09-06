using Microsoft.AspNetCore.Mvc;
using Paqueteria.Application.Modulos.Usuarios;
using Paqueteria.Application.Modulos.Usuarios.Dtos;

namespace Paqueteria.API.Controladores.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController(IUsuarioServicio servicio) : PaqueteriaControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UsuarioRespuestaDto>>> GetAll()
    {
        var result = await servicio.ObtenerTodosAsync();
        return ProcessResult(result);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UsuarioRespuestaDto>> GetById(string username)
    {
        var result = await servicio.ObtenerPorUsername(username);
        return ProcessResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioRespuestaDto>> Create([FromBody] UsuarioCrearDto dto)
    {
        var result = await servicio.AgregarAsync(dto);
        return ProcessResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UsuarioActualizarDto dto)
    {
        var result = await servicio.ActualizarAsync(dto);
        return ProcessResult(result);
    }

    [HttpDelete("{usuarioId:guid}")]
    public async Task<IActionResult> Delete(Guid usuarioId)
    {
        var result = await servicio.EliminarAsync(usuarioId);
        return ProcessResult(result);
    }
}