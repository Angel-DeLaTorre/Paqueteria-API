using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record GuiaCreateDto(
    Guid Id,
    string Clave,
    FormaPago FormaPago,
    DateTime FechaCaptura,
    DateTime? FechaPago,
    Guid ClienteOrigenId,
    Guid DireccionOrigenId,
    Guid ClienteDestinoId,
    Guid DireccionDestinoId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    Guid UsuarioAltaId,
    Guid? UsuarioCobroId,
    decimal CostoFlete,
    decimal Iva,
    decimal IvaRetenido,
    decimal Subtotal,
    decimal Total,
    decimal CobroSeguro,
    string? ImporteTexto,
    string? Observaciones,
    string? PolizaSeguro
)
{
    public Guia ToEntity()
    {
        return new Guia
        {
            Id = Id,
            Clave = Clave,
            FormaPago = FormaPago,
            FechaCaptura = FechaCaptura,
            FechaEnvio = null,
            FechaPago = FechaPago,
            ClienteOrigenId = ClienteOrigenId,
            DireccionOrigenId = DireccionOrigenId,
            ClienteDestinoId = ClienteDestinoId,
            DireccionDestinoId = DireccionDestinoId,
            SucursalOrigenId = SucursalOrigenId,
            SucursalDestinoId = SucursalDestinoId,
            UsuarioAltaId = UsuarioAltaId,
            UsuarioCobroId = UsuarioCobroId,
            CostoFlete = CostoFlete,
            Iva = Iva,
            IvaRetenido = IvaRetenido,
            Subtotal = Subtotal,
            Total = Total,
            CobroSeguro = CobroSeguro,
            ImporteTexto = ImporteTexto,
            Observaciones = Observaciones,
            PolizaSeguro = PolizaSeguro
        };
    }
}