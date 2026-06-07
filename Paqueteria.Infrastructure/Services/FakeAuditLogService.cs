using Microsoft.Extensions.Logging;
using Paqueteria.Core.Common.Audit;

namespace Paqueteria.Infrastructure.Services;

public class FakeAuditLogService(ILogger<FakeAuditLogService> logger) : IAuditLogService
{
    public void LogTrack(AuditLogEvent auditEvent)
    {
        // Por ahora solo lo mandamos al log de la consola
        logger.LogInformation("Auditoría (Pendiente BD): {Accion} por el usuario {UsuarioId}", 
            auditEvent.Accion, auditEvent.UsuarioId);
    }
}