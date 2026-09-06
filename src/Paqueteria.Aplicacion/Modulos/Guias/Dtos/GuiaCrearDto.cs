using Paqueteria.Dominio.Enums;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record GuiaCrearDto
(
    FormaPago FormaPago,
    Guid ClienteOrigenId,
    Guid DireccionOrigenId,
    Guid ClienteDestinoId,
    Guid DireccionDestinoId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    Guid UsuarioCobroId,
    decimal Flete,
    decimal CobroSeguro,
    decimal Recoleccion,
    decimal EntregaA,
    decimal Maniobras,
    decimal Peaje,
    decimal Lineas,
    bool CondonaIva,
    decimal Iva,
    decimal IvaRetenido,
    decimal Subtotal,
    decimal Total,
    string? Observaciones,
    bool EstaAsegurado,
    string? PolizaSeguro,
    Guid? SeguroId,
    
    List<ArticulosGuiaCrearDto> ArticulosGuia
);