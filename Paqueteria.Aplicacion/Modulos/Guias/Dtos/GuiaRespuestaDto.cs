using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record GuiaRespuestaDto
(
    [param: Required] Guid GuiaId,
    [param: Required] string Clave,
    [param: Required] FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    [param: Required] Guid ClienteOrigenId,
    [param: Required] ClienteRespuestaDto ClienteOrigen,
    [param: Required] DireccionGuiaSnapRespuestaDto DireccionOrigen,
    [param: Required] Guid SucursalOrigenId,
    [param: Required] string SucursalOrigenNombre,
    [param: Required] ClienteRespuestaDto ClienteDestino,
    [param: Required] DireccionGuiaSnapRespuestaDto DireccionDestino,
    [param: Required]Guid ClienteDestinoId,
    [param: Required] Guid SucursalDestinoId,
    [param: Required] string SucursalDestinoNombre,
    Guid? UsuarioCobroId,
    string? UsuarioCobroNombre,
    [param: Required] decimal Flete,
    [param: Required] decimal CobroSeguro,
    [param: Required] decimal Recoleccion,
    [param: Required] decimal EntregaA,
    [param: Required] decimal Maniobras,
    [param: Required] decimal Peaje,
    [param: Required] decimal Lineas,
    [param: Required] bool CondonaIva,
    [param: Required] decimal Iva,
    [param: Required] decimal IvaRetenido,
    [param: Required] decimal Subtotal,
    [param: Required] decimal Total,
    string? ImporteTexto,
    string? Observaciones,
    [param: Required] bool EstaAsegurado,
    string? PolizaSeguro,
    [param: Required] IEnumerable<ArticulosGuiaDto> ArticulosGuia
);