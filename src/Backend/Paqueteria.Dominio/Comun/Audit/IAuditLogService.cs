namespace Paqueteria.Dominio.Comun.Audit;

public interface IAuditLogService
{
    void LogTrack(AuditLogEvent auditEvent);
}