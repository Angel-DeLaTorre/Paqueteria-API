using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Modulos.Reportes.Constantes;
using Paqueteria.Comun.Dtos.Guias;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Paqueteria.Infrastructure.Reportes.Estrategias;

public class GuiaPdfEstrategia : IPdfEstrategia
{
    public string TipoDocumento => TipoDocumentoPdf.EtiquetaGuia;
    
    public Task<byte[]> GenerarPdfAsync<TDatos>(TDatos datos)
    {
        if (datos is not EtiquetaPaqueteRespuestaDto dto)
            throw new ArgumentException($"Los datos proporcionados deben ser del tipo '{nameof(EtiquetaPaqueteRespuestaDto)}'.", nameof(datos));

        var pdfBytes = Document.Create(contenedor =>
        {
            contenedor.Page(pagina =>
            {
                // Configurar tamaño exacto de etiqueta: 10cm x 20cm (100mm x 200mm)
                pagina.Size(new PageSize(100, 200, Unit.Millimetre));
                pagina.Margin(4, Unit.Millimetre);
                pagina.PageColor(Colors.White);
                pagina.DefaultTextStyle(estilo => estilo.FontSize(9).FontFamily(Fonts.Arial));

                pagina.Content().Column(columna =>
                {
                    // -------------------------------------------------------------
                    // 1. ENCABEZADO (Logo, Fecha, Tipo Pago y Folio)
                    // -------------------------------------------------------------
                    columna.Item().Row(fila =>
                    {
                        fila.RelativeItem(2).Column(c =>
                        {
                            c.Item().Text("RIINPACK").Bold().FontSize(18).FontColor(Colors.Red.Medium);
                            c.Item().Text("EXPRESS").Bold().FontSize(10).FontColor(Colors.Blue.Darken2);
                        });

                        fila.RelativeItem(2).AlignRight().Column(c =>
                        {
                            c.Item().Text(dto.FechaCaptura.ToString("dd/MM/yyyy")).Bold().FontSize(10);
                            c.Item().Text(dto.FormaPago.ToUpper()).Bold().FontSize(10);
                            c.Item().Text($".{dto.ClaveGuia}").Bold().FontSize(11);
                        });
                    });

                    columna.Item().PaddingVertical(3).LineHorizontal(1);

                    // -------------------------------------------------------------
                    // 2. REMITENTE
                    // -------------------------------------------------------------
                    columna.Item().Column(c =>
                    {
                        c.Item().Text(x =>
                        {
                            x.Span("Remitente: ").FontSize(10);
                            x.Span(dto.Origen.ToUpper()).Bold().FontSize(13);
                        });
                        c.Item().Text(dto.Remitente.ToUpper()).FontSize(9);
                        c.Item().Text(dto.DireccionOrigen.ToUpper()).FontSize(8.5f);

                        c.Item().PaddingTop(3).Row(r =>
                        {
                            r.RelativeItem().Text($"RFC: {dto.RfcRemitente ?? "*"}").FontSize(8.5f);
                            r.RelativeItem().Text($"TEL: {dto.TelefonoRemitente ?? "*"}").FontSize(8.5f);
                        });
                    });

                    columna.Item().PaddingVertical(3).LineHorizontal(1.5f);

                    // -------------------------------------------------------------
                    // 3. DESTINO
                    // -------------------------------------------------------------
                    columna.Item().Column(c =>
                    {
                        c.Item().Text(x =>
                        {
                            x.Span("Destino: ").FontSize(10);
                            x.Span(dto.Destino.ToUpper()).Bold().FontSize(13);
                        });
                        c.Item().Text(dto.Destinatario.ToUpper()).FontSize(9);
                        c.Item().Text(dto.DireccionDestino.ToUpper()).FontSize(8.5f);

                        c.Item().PaddingTop(3).Row(r =>
                        {
                            r.RelativeItem().Text($"RFC: {dto.RfcDestinatario ?? "*"}").FontSize(8.5f);
                            r.RelativeItem().Text($"TEL: {dto.TelefonoDestinatario ?? "*"}").FontSize(8.5f);
                        });
                    });

                    columna.Item().PaddingVertical(3).LineHorizontal(1.5f);

                    // -------------------------------------------------------------
                    // 4. DESCRIPCIÓN Y ARTÍCULOS
                    // -------------------------------------------------------------
                    columna.Item().Column(c =>
                    {
                        c.Item().AlignCenter().Text("Descripción:").Bold().FontSize(10);
                        
                        foreach (var art in dto.Articulos)
                        {
                            c.Item().Text($"{art.Cantidad} {art.Descripcion.ToUpper()}").FontSize(8.5f);
                        }
                    });

                    columna.Item().PaddingVertical(3).LineHorizontal(1);

                    // -------------------------------------------------------------
                    // 5. OBSERVACIONES Y PIE CON FOLIO / NUMERACIÓN
                    // -------------------------------------------------------------
                    columna.Item().Column(c =>
                    {
                        c.Item().AlignCenter().Text("Observaciones:").Bold().FontSize(10);
                        if (!string.IsNullOrWhiteSpace(dto.Observaciones))
                        {
                            c.Item().Text(dto.Observaciones).FontSize(8.5f);
                        }
                    });

                    // Empujar el pie de la etiqueta hacia el fondo
                    columna.Item().PaddingTop(15).Row(r =>
                    {
                        r.RelativeItem().Text(dto.FolioInterno).Bold().FontSize(22);
                        r.RelativeItem().AlignRight().Text($"{dto.NumeroPaquete} / {dto.TotalPaquetes}").FontSize(14);
                    });
                });
            });
        }).GeneratePdf();

        return Task.FromResult(pdfBytes);
    }
}