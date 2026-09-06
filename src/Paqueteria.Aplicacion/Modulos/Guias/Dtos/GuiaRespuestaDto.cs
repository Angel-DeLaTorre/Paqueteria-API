using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record GuiaRespuestaDto
(
    Guid GuiaId,
    string Clave,
    FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    Guid ClienteOrigenId,
    ClienteRespuestaDto ClienteOrigen,
    DireccionGuiaSnapRespuestaDto DireccionOrigen,
    Guid SucursalOrigenId,
    string SucursalOrigenNombre,
    ClienteRespuestaDto ClienteDestino,
    DireccionGuiaSnapRespuestaDto DireccionDestino,
    Guid ClienteDestinoId,
    Guid SucursalDestinoId,
    string SucursalDestinoNombre,
    Guid? UsuarioCobroId,
    string? UsuarioCobroNombre,
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
    string? ImporteTexto,
    string? Observaciones,
    bool EstaAsegurado,
    string? PolizaSeguro,
    IEnumerable<ArticulosGuiaDto> ArticulosGuia
);