using Paqueteria.Application.DTOs;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record GuiaRespuestaDto
(
    Guid GuiaId,
    string Clave,
    FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    Guid ClienteOrigenId,
    ClienteResponseDto ClienteOrigen,
    DireccionGuiaSnapResponseDto DireccionOrigen,
    Guid SucursalOrigenId,
    string SucursalOrigenNombre,
    ClienteResponseDto ClienteDestino,
    DireccionGuiaSnapResponseDto DireccionDestino,
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
)
{
    public static GuiaResponseDto FromEntity(Guia entity)
    {
        var clienteOrigen = ClienteResponseDto.FromEntity(entity.ClienteOrigen);
        var direccionOrigen = DireccionGuiaSnapResponseDto.FromEntity(entity.DireccionOrigen);
        
        var clienteDestino = ClienteResponseDto.FromEntity(entity.ClienteOrigen);
        var direccionDestino = DireccionGuiaSnapResponseDto.FromEntity(entity.DireccionDestino);

        var articulos = entity.ArticulosGuia.Select( ArticulosGuiaDto.FromEntity );
        
        return new GuiaResponseDto(
            entity.Id,
            entity.Clave,
            entity.FormaPago,
            entity.FechaEnvio,
            entity.FechaPago,
            entity.ClienteOrigenId,
            clienteOrigen,
            direccionOrigen,
            entity.SucursalOrigenId,
            entity.SucursalDestino.Nombre,
            clienteDestino,
            direccionDestino,
            entity.ClienteDestinoId,
            entity.SucursalDestinoId,
            entity.SucursalDestino.Nombre,
            entity.UsuarioCobroId,
            entity.UsuarioCobro?.Nombre,
            entity.Flete,
            entity.CobroSeguro,
            entity.Recoleccion,
            entity.EntregaA,
            entity.Maniobras,
            entity.Peaje,
            entity.Lineas,
            entity.CondonaIva,
            entity.Iva,
            entity.IvaRetenido,
            entity.Subtotal,
            entity.Total,
            entity.ImporteTexto,
            entity.Observaciones,
            entity.EstaAsegurado,
            entity.PolizaSeguro,
            articulos
        );
    }
}