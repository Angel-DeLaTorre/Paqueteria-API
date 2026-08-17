namespace Paqueteria.Application.DTOs.Sincronizacion;

public record EventoSyncDto(
    string Entidad,
    string Operacion,
    object Datos,
    DateTime FechaEvento
);