namespace Paqueteria.Application.Dtos.Reportes;

public record EtiquetaPaqueteDto(
    string ClaveGuia,
    string FolioInterno,
    DateTime FechaCaptura,
    string FormaPago, // Ej. "PREPAGADA", "PAGO EN DESTINO"
    
    // Remitente
    string Origen, // Ej. "LEON GTO"
    string Remitente,
    string DireccionOrigen,
    string? RfcRemitente,
    string? TelefonoRemitente,
    
    // Destino
    string Destino, // Ej. "SAN LUIS POTOSI, SLP"
    string Destinatario,
    string DireccionDestino,
    string? RfcDestinatario,
    string? TelefonoDestinatario,
    
    // Artículos
    List<ArticuloEtiquetaItemDto> Articulos,
    
    // Pie
    string? Observaciones,
    int NumeroPaquete,
    int TotalPaquetes
);

public record ArticuloEtiquetaItemDto(int Cantidad, string Descripcion);