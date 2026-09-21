namespace Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Dtos;

public record RegistrarIngresoTransbordoDto
(
    Guid GuiaId,
    Guid AsignacionId,
    Guid SucursalTransbordoId,
    string? Observaciones = null
);