namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record EtiquetaPaqueteRespuestaDto
(
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
    List<ArticuloEtiquetaDto> Articulos,

    // Pie
    string? Observaciones,
    int NumeroPaquete,
    int TotalPaquetes
);