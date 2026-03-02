using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record GuiaCreateDto(
    string Clave,
    FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    Guid ClienteOrigenId,
    DireccionGuiaSnapCreateDto DireccionOrigen,
    Guid ClienteDestinoId,
    DireccionGuiaSnapCreateDto DireccionDestino,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    //Guid UsuarioAltaId,
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
    public Guia ToEntity() =>
        new ()
        {
            Clave = Clave,
            FormaPago = FormaPago,
            FechaEnvio = FechaEnvio,
            FechaPago = FechaPago,
            ClienteOrigenId = ClienteOrigenId,
            DireccionOrigen = DireccionOrigen.ToEntity(),
            ClienteDestinoId = ClienteDestinoId,
            DireccionDestino = DireccionDestino.ToEntity(),
            SucursalOrigenId = SucursalOrigenId,
            SucursalDestinoId = SucursalDestinoId,
            //UsuarioAltaId = UsuarioAltaId,
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

public record GuiaUpdateDto(
    Guid GuiaId,
    string Clave,
    FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    Guid ClienteOrigenId,
    //DireccionGuiaSnapCreateDto DireccionOrigen,
    Guid ClienteDestinoId,
    //DireccionGuiaSnapCreateDto DireccionDestino,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    //Guid? UsuarioCobroId,
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
    public void UpdateEntity(Guia entity)
    {
        entity.Clave = Clave;
        entity.FormaPago = FormaPago;
        entity.FechaEnvio = FechaEnvio;
        entity.FechaPago = FechaPago;
        entity.ClienteOrigenId = ClienteOrigenId;
        entity.ClienteDestinoId = ClienteDestinoId;
        entity.SucursalOrigenId = SucursalOrigenId;
        entity.SucursalDestinoId = SucursalDestinoId;
        entity.CostoFlete = CostoFlete;
        entity.Iva = Iva;
        entity.IvaRetenido = IvaRetenido;
        entity.Subtotal = Subtotal;
        entity.Total = Total;
        entity.CobroSeguro = CobroSeguro;
        entity.ImporteTexto = ImporteTexto;
        entity.Observaciones = Observaciones;
        entity.PolizaSeguro = PolizaSeguro;
    }
};

public record GuiaResponseDto(
    Guid GuiaId,
    string Clave,
    FormaPago FormaPago,
    DateTime? FechaEnvio,
    DateTime? FechaPago,
    Guid ClienteOrigenId,
    DireccionGuiaSnapResponseDto DireccionOrigen,
    Guid SucursalOrigenId,
    string SucursalOrigenNombre,
    DireccionGuiaSnapResponseDto DireccionDestino,
    Guid ClienteDestinoId,
    Guid SucursalDestinoId,
    string SucursalDestinoNombre,
    Guid? UsuarioCobroId,
    string? UsuarioCobroNombre,
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
    public static GuiaResponseDto FromEntity(Guia entity) =>
        new GuiaResponseDto(
            entity.Id,
            entity.Clave,
            entity.FormaPago,
            entity.FechaEnvio,
            entity.FechaPago,
            entity.ClienteOrigenId,
            DireccionGuiaSnapResponseDto.FromEntity(entity.DireccionOrigen),
            entity.SucursalOrigenId,
            entity.SucursalDestino.Nombre,
            DireccionGuiaSnapResponseDto.FromEntity(entity.DireccionDestino),
            entity.ClienteDestinoId,
            entity.SucursalDestinoId,
            entity.SucursalDestino.Nombre,
            entity.UsuarioCobroId,
            entity.UsuarioCobro?.Nombre,
            entity.CostoFlete,
            entity.Iva,
            entity.IvaRetenido,
            entity.Subtotal,
            entity.Total,
            entity.CobroSeguro,
            entity.ImporteTexto,
            entity.Observaciones,
            entity.PolizaSeguro
        );
}