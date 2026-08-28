namespace Paqueteria.Core.Comun.Audit;

public interface IAuditLogService
{
    void LogTrack(AuditLogEvent auditEvent);
}