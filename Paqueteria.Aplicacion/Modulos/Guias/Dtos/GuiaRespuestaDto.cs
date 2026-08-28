using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record GuiaRespuestaDto
(
    [property: Required] Guid GuiaId,
    [property: Required] string Clave,
    [property: Required] FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    [property: Required] Guid ClienteOrigenId,
    [property: Required] ClienteRespuestaDto ClienteOrigen,
    [property: Required] DireccionGuiaSnapRespuestaDto DireccionOrigen,
    [property: Required] Guid SucursalOrigenId,
    [property: Required] string SucursalOrigenNombre,
    [property: Required] ClienteRespuestaDto ClienteDestino,
    [property: Required] DireccionGuiaSnapRespuestaDto DireccionDestino,
    [property: Required]Guid ClienteDestinoId,
    [property: Required] Guid SucursalDestinoId,
    [property: Required] string SucursalDestinoNombre,
    Guid? UsuarioCobroId,
    string? UsuarioCobroNombre,
    [property: Required] decimal Flete,
    [property: Required] decimal CobroSeguro,
    [property: Required] decimal Recoleccion,
    [property: Required] decimal EntregaA,
    [property: Required] decimal Maniobras,
    [property: Required] decimal Peaje,
    [property: Required] decimal Lineas,
    [property: Required] bool CondonaIva,
    [property: Required] decimal Iva,
    [property: Required] decimal IvaRetenido,
    [property: Required] decimal Subtotal,
    [property: Required] decimal Total,
    string? ImporteTexto,
    string? Observaciones,
    [property: Required] bool EstaAsegurado,
    string? PolizaSeguro,
    [property: Required] IEnumerable<ArticulosGuiaDto> ArticulosGuia
);