namespace Paqueteria.Core.Common.Audit;

public interface IAuditLogService
{
    void LogTrack(AuditLogEvent auditEvent);
}