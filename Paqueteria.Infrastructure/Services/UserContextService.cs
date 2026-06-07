using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService

{
    public Guid UserId => GetGuidClaim(ClaimTypes.NameIdentifier);
    public string Username => httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";
    public Guid SucursalId => GetGuidClaim(ClaimTypes.Sid);
    public Guid EmpresaId => GetGuidClaim("empresa_id");
    public string IpAddress => httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

    private Guid GetGuidClaim(string claimType)
    {
        var claim = httpContextAccessor.HttpContext?.User?.FindFirst(claimType);
        if (claim == null) return Guid.Empty;
        var value = claim.Value; 
        return Guid.TryParse(value, out var guid) ? guid : Guid.Empty;
    }
}