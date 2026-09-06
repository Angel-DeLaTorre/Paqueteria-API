namespace Paqueteria.Dominio.Comun.Audit;

public record AuditLogEvent(
    Guid UsuarioId,
    Guid EmpresaId,
    Guid? SucursalId,
    string Accion,
    string Modulo,
    string Detalle,
    string IpAddress
);