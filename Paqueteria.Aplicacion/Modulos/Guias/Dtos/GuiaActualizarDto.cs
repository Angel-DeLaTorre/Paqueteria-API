using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record GuiaActualizarDto
(
    [param: Required] Guid GuiaId,
    FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    Guid ClienteOrigenId,
    Guid ClienteDestinoId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    decimal CostoFlete,
    decimal Iva,
    decimal IvaRetenido,
    decimal Subtotal,
    decimal Total,
    decimal CobroSeguro,
    string? ImporteTexto,
    string? Observaciones,
    string? PolizaSeguro    
);