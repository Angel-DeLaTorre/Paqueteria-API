using System.Security.Claims;
using Paqueteria.Aplicacion.Comun.Interfaces;

namespace Paqueteria.API.Servicios;

public class UsuarioContextoServicio(IHttpContextAccessor httpContextAccessor) : IUsuarioContextoServicio
{
    public Guid UsuarioId => ObtenerGuidClaim(ClaimTypes.NameIdentifier);
    public string Username => httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonimo";
    public Guid SucursalId => ObtenerGuidClaim(ClaimTypes.Sid);
    public Guid EmpresaId => ObtenerGuidClaim("empresa_id");
    public string DireccionIp => httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Desconocida";

    private Guid ObtenerGuidClaim(string tipoClaim)
    {
        var claim = httpContextAccessor.HttpContext?.User?.FindFirst(tipoClaim);
        if (claim == null) return Guid.Empty;
        return Guid.TryParse(claim.Value, out var guid) ? guid : Guid.Empty;
    }
}