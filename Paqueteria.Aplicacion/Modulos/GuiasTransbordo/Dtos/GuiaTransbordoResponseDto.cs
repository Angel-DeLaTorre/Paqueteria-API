namespace Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;

public record GuiaTransbordoResponseDto
(
    Guid Id,
    Guid GuiaId,
    Guid AsignacionId,
    Guid SucursalTransbordoId,
    string SucursalNombre,
    DateTime FechaEscaneoIngreso,
    DateTime? FechaEscaneoSalida,
    string? Observaciones
);