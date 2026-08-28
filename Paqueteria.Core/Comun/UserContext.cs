namespace Paqueteria.Core.Comun;

public record UserContext(
    Guid UserId,
    string? Username,
    Guid Sucursal,
    string IpAddress,
    Guid EmpresaId
);