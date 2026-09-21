namespace Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Dtos;

public record GuiaTransbordoRespuestaDto
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