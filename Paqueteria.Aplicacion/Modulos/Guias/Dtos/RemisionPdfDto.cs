namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record RemisionPdfDto(
// Encabezado
    string ClaveGuia,
    DateTime FechaCaptura,
    string FormaPago,
    string Estatus,

// Remitente (Origen)
    string OrigenCiudad,
    string RemitenteNombre,
    string? RfcRemitente,
    string DireccionOrigen,
    string TelefonoRemitente,

// Destinatario (Destino)
    string DestinoCiudad,
    string DestinatarioNombre,
    string? RfcDestinatario,
    string DireccionDestino,
    string TelefonoDestino,

// Artículos
    List<ItemArticuloRemisionDto> Articulos,

// Desglose Financiero
    decimal Flete,
    decimal Recoleccion,
    decimal EntregaA,
    decimal Maniobras,
    decimal Peaje,
    decimal Lineas,
    decimal Subtotal,
    decimal Iva,
    decimal IvaRetenido,
    decimal Total,
    string ImporteTexto,

// Pie / Información Adicional
    string UsuarioCaptura,
    string? Observaciones
);

public record ItemArticuloRemisionDto(
    int Cantidad,
    string Descripcion,
    decimal ValorUnidad,
    decimal PesoUnitarioKg,
    decimal Importe
);