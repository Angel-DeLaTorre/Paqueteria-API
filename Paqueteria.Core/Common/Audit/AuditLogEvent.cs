namespace Paqueteria.Core.Common.Audit;

public record AuditLogEvent(
    Guid UsuarioId,
    Guid EmpresaId,
    Guid? SucursalId,
    string Accion,
    string Modulo,
    string Detalle,
    string IpAddress
);