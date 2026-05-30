using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class PaqueteriaControllerBase : ControllerBase
{
    /*
    protected UserContext CurrentUser => new UserContext(
        UserId: Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty),
        Username: User.FindFirstValue(ClaimTypes.Name),
        Sucursal: Guid.Parse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty),
        IpAddress: HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
        EmpresaId: Guid.Parse(User.FindFirstValue(ClaimTypes.System) ?? string.Empty)
    );
    */
    
    private Guid GetGuidClaim(string claimType)
    {
        var value = User.FindFirstValue(claimType);
        return Guid.TryParse(value, out var guid) ? guid : Guid.Empty;
    }
    
    protected UserContext CurrentUser => new UserContext(
        UserId: GetGuidClaim(ClaimTypes.NameIdentifier),
        Username: User.FindFirstValue(ClaimTypes.Name) ?? "Anonymous",
        Sucursal: GetGuidClaim(ClaimTypes.Sid), // Ojo: Asegúrate de que el token lo traiga
        IpAddress: HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
        EmpresaId: GetGuidClaim("empresa_id")
    );

    protected ActionResult<T> ProcessResult<T>(Result<T> result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    protected IActionResult ProcessResult(Result result)
    {
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    
}