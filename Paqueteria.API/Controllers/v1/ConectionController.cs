using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Paqueteria.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class ConectionController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Ping()
    {
        return Task.FromResult<IActionResult>(Ok("Pong"));
    }
}