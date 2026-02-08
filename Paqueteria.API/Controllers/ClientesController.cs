using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var clientes = await context.Clientes.ToListAsync();
        return Ok(clientes);
    }
}