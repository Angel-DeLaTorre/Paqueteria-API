using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Modulos.Reportes.Constantes;
using Paqueteria.Application.Modulos.Guias.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Paqueteria.Infrastructure.Reportes.Estrategias;

public class RemisionPdfEstrategia : IPdfEstrategia
{
    public string TipoDocumento => TipoDocumentoPdf.RemisionGuia;
    
    public Task<byte[]> GenerarPdfAsync<TDatos>(TDatos datos)
    {
        if (datos is not RemisionPdfDto dto)
            throw new ArgumentException($"Los datos proporcionados deben ser del tipo '{nameof(RemisionPdfDto)}'.", nameof(datos));

        var pdfBytes = Document.Create(contenedor =>
        {
            contenedor.Page(pagina =>
            {
                // Configuración de tamaño carta u opción personalizada
                pagina.Size(PageSizes.Letter);
                pagina.Margin(8, Unit.Millimetre);
                pagina.PageColor(Colors.White);
                pagina.DefaultTextStyle(x => x.FontSize(8.5f).FontFamily(Fonts.Arial));

                pagina.Content().Column(columna =>
                {
                    // -------------------------------------------------------------
                    // 1. SECCIÓN ORIGEN Y DESTINO (ENCABEZADO DERECHO)
                    // -------------------------------------------------------------
                    columna.Item().Row(fila =>
                    {
                        // Datos Origen (Izquierda)
                        fila.RelativeItem(3).Border(1).BorderColor(Colors.Grey.Medium).Padding(4).Column(c =>
                        {
                            c.Item().Text(dto.OrigenCiudad.ToUpper()).Bold().FontSize(11);
                            c.Item().Text(dto.RemitenteNombre.ToUpper()).Bold();
                            c.Item().Text($"RFC: {dto.RfcRemitente ?? "*"}");
                            c.Item().Text(dto.DireccionOrigen.ToUpper());
                            c.Item().Text($"TEL: {dto.TelefonoRemitente}");
                        });

                        fila.ConstantItem(10); // Espaciador

                        // Datos Destino (Centro)
                        fila.RelativeItem(3).Border(1).BorderColor(Colors.Grey.Medium).Padding(4).Column(c =>
                        {
                            c.Item().Text(dto.DestinoCiudad.ToUpper()).Bold().FontSize(11);
                            c.Item().Text(dto.DestinatarioNombre.ToUpper()).Bold();
                            c.Item().Text($"RFC: {dto.RfcDestinatario ?? "*"}");
                            c.Item().Text(dto.DireccionDestino.ToUpper());
                            c.Item().Text($"TEL: {dto.TelefonoDestino}");
                        });

                        fila.ConstantItem(10); // Espaciador

                        // Bloque Folio / Fecha / Pago (Derecha)
                        fila.RelativeItem(2).Border(1).BorderColor(Colors.Grey.Medium).Padding(4).Column(c =>
                        {
                            c.Item().AlignCenter().Text($"FOLIO: {dto.ClaveGuia}").Bold().FontSize(12).FontColor(Colors.Red.Medium);
                            c.Item().PaddingTop(2).Text($"FECHA: {dto.FechaCaptura:dd/MM/yyyy}").FontSize(8);
                            c.Item().Text($"PAGO: {dto.FormaPago.ToUpper()}").Bold().FontSize(8);
                            c.Item().Text($"ESTATUS: {dto.Estatus.ToUpper()}").FontSize(8);
                        });
                    });

                    columna.Item().PaddingVertical(5);

                    // -------------------------------------------------------------
                    // 2. TABLA DE ARTÍCULOS E IMPORTES LATERALES
                    // -------------------------------------------------------------
                    columna.Item().Row(fila =>
                    {
                        // Tabla Principal de Productos / Cantidades
                        fila.RelativeItem(6).Border(1).BorderColor(Colors.Grey.Medium).Table(tabla =>
                        {
                            tabla.ColumnsDefinition(col =>
                            {
                                col.ConstantColumn(35); // Cantidad
                                col.RelativeColumn();   // Descripción
                                col.ConstantColumn(50); // Valor / P.U.
                                col.ConstantColumn(50); // Importe
                            });

                            // Encabezado Tabla
                            tabla.Header(h =>
                            {
                                h.Cell().BorderBottom(1).Padding(2).Text("CANT").Bold();
                                h.Cell().BorderBottom(1).Padding(2).Text("DESCRIPCIÓN").Bold();
                                h.Cell().BorderBottom(1).Padding(2).AlignRight().Text("P.U.").Bold();
                                h.Cell().BorderBottom(1).Padding(2).AlignRight().Text("IMPORTE").Bold();
                            });

                            // Filas de Artículos
                            foreach (var art in dto.Articulos)
                            {
                                tabla.Cell().Padding(2).Text(art.Cantidad.ToString());
                                tabla.Cell().Padding(2).Text(art.Descripcion.ToUpper());
                                tabla.Cell().Padding(2).AlignRight().Text(art.ValorUnidad.ToString("C2"));
                                tabla.Cell().Padding(2).AlignRight().Text(art.Importe.ToString("C2"));
                            }
                        });

                        fila.ConstantItem(10); // Espaciador

                        // Tabla de Desglose de Gastos (Flete, Maniobras, IVA, etc.)
                        fila.RelativeItem(2).Border(1).BorderColor(Colors.Grey.Medium).Padding(4).Column(c =>
                        {
                            c.Item().Row(r => { r.RelativeItem().Text("FLETE:"); r.RelativeItem().AlignRight().Text(dto.Flete.ToString("C2")); });
                            c.Item().Row(r => { r.RelativeItem().Text("RECOLECCIÓN:"); r.RelativeItem().AlignRight().Text(dto.Recoleccion.ToString("C2")); });
                            c.Item().Row(r => { r.RelativeItem().Text("ENTREGA A:"); r.RelativeItem().AlignRight().Text(dto.EntregaA.ToString("C2")); });
                            c.Item().Row(r => { r.RelativeItem().Text("MANIOBRAS:"); r.RelativeItem().AlignRight().Text(dto.Maniobras.ToString("C2")); });
                            c.Item().Row(r => { r.RelativeItem().Text("PEAJE:"); r.RelativeItem().AlignRight().Text(dto.Peaje.ToString("C2")); });
                            c.Item().Row(r => { r.RelativeItem().Text("LINEAS:"); r.RelativeItem().AlignRight().Text(dto.Lineas.ToString("C2")); });

                            c.Item().LineHorizontal(0.5f);

                            c.Item().Row(r => { r.RelativeItem().Text("SUBTOTAL:").Bold(); r.RelativeItem().AlignRight().Text(dto.Subtotal.ToString("C2")).Bold(); });
                            c.Item().Row(r => { r.RelativeItem().Text("I.V.A.:"); r.RelativeItem().AlignRight().Text(dto.Iva.ToString("C2")); });
                            c.Item().Row(r => { r.RelativeItem().Text("IVA RET.:"); r.RelativeItem().AlignRight().Text(dto.IvaRetenido.ToString("C2")); });

                            c.Item().LineHorizontal(1);

                            c.Item().Row(r => { r.RelativeItem().Text("TOTAL:").Bold().FontSize(10); r.RelativeItem().AlignRight().Text(dto.Total.ToString("C2")).Bold().FontSize(10); });
                        });
                    });

                    columna.Item().PaddingVertical(4);

                    // -------------------------------------------------------------
                    // 3. IMPORTE EN LETRA Y PIE DE PÁGINA
                    // -------------------------------------------------------------
                    columna.Item().Border(1).BorderColor(Colors.Grey.Medium).Padding(4).Column(c =>
                    {
                        c.Item().Text(x =>
                        {
                            x.Span("IMPORTE CON LETRA: ").Bold();
                            x.Span(dto.ImporteTexto.ToUpper());
                        });
                        
                        if (!string.IsNullOrWhiteSpace(dto.Observaciones))
                        {
                            c.Item().PaddingTop(2).Text($"OBSERVACIONES: {dto.Observaciones.ToUpper()}").FontSize(8);
                        }
                    });

                    columna.Item().PaddingTop(6).Row(r =>
                    {
                        r.RelativeItem().Text($"CAPTURADO POR: {dto.UsuarioCaptura.ToUpper()}").FontSize(8);
                        r.RelativeItem().AlignRight().Text($"FECHA DE IMPRESIÓN: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(8);
                    });
                });
            });
        }).GeneratePdf();

        return Task.FromResult(pdfBytes);
    }
}