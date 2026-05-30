namespace Paqueteria.API.Middlewares;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System;
using Npgsql;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Ocurrió un fallo no controlado: {Message}", exception.Message);
        
        var (statusCode, errorCode, message) = exception switch
        {
            DbUpdateException { InnerException: PostgresException pgEx } => pgEx.SqlState switch
            {
                // Clave duplicada (Ej: Intentar registrar un correo que ya existe y es UNIQUE)
                PostgresErrorCodes.UniqueViolation => 
                    (StatusCodes.Status409Conflict, "DB_DUPLICATE_ENTRY", "El registro ya existe en el sistema."),

                // Violación de Clave Foránea (Ej: Intentar registrar una compra para un cliente que no existe)
                PostgresErrorCodes.ForeignKeyViolation => 
                    (StatusCodes.Status400BadRequest, "DB_FOREIGN_KEY_VIOLATION", "Operación inválida: El registro relacionado no existe."),

                // Violación de restricción NOT NULL (Ej: Campo obligatorio vacío a nivel BD)
                PostgresErrorCodes.NotNullViolation => 
                    (StatusCodes.Status400BadRequest, "DB_REQUIRED_FIELD_MISSING", "Uno o más campos obligatorios no fueron proporcionados."),

                // Violación de Check Constraint (Ej: Edad < 18 y la BD exige mayores de edad)
                PostgresErrorCodes.CheckViolation => 
                    (StatusCodes.Status400BadRequest, "DB_CONSTRAINT_CHECK_FAILED", "Los datos proporcionados no cumplen con las reglas de validación del sistema."),
                
                _ => (StatusCodes.Status500InternalServerError, "DB_UPDATE_ERROR", "Ocurrió un error al persistir los cambios en la base de datos.")
            },
            
            DbUpdateException { InnerException: NpgsqlException npgsqlInner } when IsDatabaseDown(npgsqlInner) =>
                (StatusCodes.Status503ServiceUnavailable, "ERR_DATABASE_DOWN", "El servidor de base de datos PostgreSQL se encuentra fuera de línea o apagado."),
            
            InvalidOperationException { InnerException: NpgsqlException npgsqlTransient } when IsDatabaseDown(npgsqlTransient) =>
                (StatusCodes.Status503ServiceUnavailable, "ERR_DATABASE_DOWN", "El servicio de Paquetería Web se encuentra temporalmente fuera de línea por mantenimiento de la base de datos."),
            
            NpgsqlException npgsqlEx => IsDatabaseDown(npgsqlEx)
                ? (StatusCodes.Status503ServiceUnavailable, "ERR_DATABASE_DOWN", "El servidor de base de datos PostgreSQL se encuentra fuera de línea o apagado.")
                : (npgsqlEx.InnerException is IOException 
                    ? (StatusCodes.Status503ServiceUnavailable, "DB_CONNECTION_LOST", "La conexión con el servidor de datos se interrumpió abruptamente.") 
                    : (StatusCodes.Status503ServiceUnavailable, "DB_SERVER_UNAVAILABLE", "El motor de base de datos PostgreSQL no responde.")),
            
            TimeoutException => 
                (StatusCodes.Status504GatewayTimeout, "SYSTEM_TIMEOUT", "El servidor tardó demasiado tiempo en procesar la solicitud."),

            // ERROR GENÉRICO DE RESPALDO (Cualquier bug de código como NullReferenceException)
            _ => 
                (StatusCodes.Status500InternalServerError, "SYSTEM_ERROR", "Ocurrió un error interno inesperado en el servidor.")
        };


        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        
        var response = new
        {
            IsSuccess = false,
            Body = (object?)null,
            DetalleError = new
            {
                Code = errorCode,
                Description = message
            }
        };
        
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
    
    /// <summary>
    /// Función utilitaria privada para evaluar si una NpgsqlException fue provocada por un servidor apagado o inaccesible
    /// </summary>
    private static bool IsDatabaseDown(NpgsqlException ex)
    {
        // Evalúa si la causa raíz es un fallo de Socket (Connection Refused / Timeout de red)
        if (ex.InnerException is SocketException socketEx)
        {
            return socketEx.SocketErrorCode is SocketError.ConnectionRefused 
                or SocketError.TimedOut 
                or SocketError.HostUnreachable;
        }

        // Evalúa códigos de estado de Postgres específicos de inicialización/autenticación fallida por caída
        // O strings comunes de error que levanta el Driver cuando el puerto (5432 por defecto) está cerrado.
        var message = ex.Message.ToLower();
        return message.Contains("failed to connect") || 
               message.Contains("connection refused") || 
               message.Contains("is the server running");
    }
}
