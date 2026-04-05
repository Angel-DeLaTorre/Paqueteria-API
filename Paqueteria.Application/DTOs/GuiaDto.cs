using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record GuiaCreateDto(
    string? Clave,
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
    string ImporteTexto,
    string? Observaciones,
    string? PolizaSeguro,
    Guid? Seguro
)
{
    public Guia ToEntity(Guid usuarioAltaId, Guid empresaId)
    {
        var direccionOrigen = DireccionOrigen.ToEntity();
        var direccionDestino = DireccionDestino.ToEntity();
        
        return Guia.Create(
            FormaPago,
            FechaPago,
            ClienteOrigenId,
            direccionOrigen.Id,
            ClienteDestinoId,
            direccionDestino.Id,
            SucursalOrigenId,
            SucursalDestinoId,
            usuarioAltaId,
            UsuarioCobroId,
            CostoFlete,
            Iva,
            IvaRetenido,
            Subtotal,
            Total,
            CobroSeguro,
            ImporteTexto,
            Observaciones,
            PolizaSeguro,
            Seguro,
            empresaId
        );
    }
}

public record GuiaUpdateDto(
    Guid GuiaId,
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