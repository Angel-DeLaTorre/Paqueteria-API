namespace Paqueteria.Application.Dtos.Sincronizacion;

public record EventoSyncDto(
    string Entidad,
    string Operacion,
    object Datos,
    DateTime FechaEvento
);