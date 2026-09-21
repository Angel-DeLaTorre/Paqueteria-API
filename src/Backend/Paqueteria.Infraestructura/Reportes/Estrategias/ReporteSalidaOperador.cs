using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Modulos.Reportes.Constantes;
using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Entidades.Remisiones;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Paqueteria.Infrastructure.Reportes.Estrategias;

public class ReporteSalidaOperador : IPdfEstrategia
{
    public string TipoDocumento => TipoDocumentoPdf.ReporteSalidaOperador;

    public Task<byte[]> GenerarPdfAsync<TDatos>(TDatos datos)
    {
        if (datos is not IEnumerable<Asignacion> asignaciones)
            throw new ArgumentException($"Los datos proporcionados deben ser del tipo '{nameof(IEnumerable<Asignacion>)}'.", nameof(datos));

        var listaAsignaciones = asignaciones.ToList();

        var pdfBytes = Document.Create(contenedor =>
        {
            contenedor.Page(pagina =>
            {
                pagina.Size(PageSizes.Letter.Landscape());
                pagina.Margin(12, Unit.Millimetre);
                pagina.PageColor(Colors.White);
                pagina.DefaultTextStyle(estilo => estilo.FontSize(8).FontFamily(Fonts.Arial));

                pagina.Header().Element(c => ConstruirEncabezado(c, listaAsignaciones));
                pagina.Content().Element(c => ConstruirContenido(c, listaAsignaciones));
                pagina.Footer().Element(ConstruirPiePagina);
            });
        }).GeneratePdf();

        return Task.FromResult(pdfBytes);
    }

    private void ConstruirEncabezado(IContainer contenedor, List<Asignacion> asignaciones)
    {
        var fechaMin = asignaciones.SelectMany(a => a.Guias).Select(g => (DateTime?)g.FechaCaptura).Min() ?? DateTime.Today;
        var fechaMax = asignaciones.SelectMany(a => a.Guias).Select(g => (DateTime?)g.FechaCaptura).Max() ?? DateTime.Today;

        contenedor.Column(columna =>
        {
            columna.Item().Row(fila =>
            {
                fila.RelativeItem().Text($"{DateTime.Now:dd/MM/yyyy} RIINPACK").Bold().FontSize(9);
                fila.RelativeItem().AlignRight().Text(texto =>
                {
                    texto.Span("HOJA : ");
                    texto.CurrentPageNumber();
                });
            });

            columna.Item().Text($"Reporte de Salidas por Operador del {fechaMin:dd 'de' MMM 'del' yyyy} al {fechaMax:dd 'de' MMM 'del' yyyy}".ToUpper())
                .FontSize(10).Bold().AlignCenter();

            columna.Item().PaddingVertical(4).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);
        });
    }

    private void ConstruirContenido(IContainer contenedor, List<Asignacion> asignaciones)
    {
        contenedor.PaddingVertical(4).Column(columna =>
        {
            int numeroOperador = 1;
            foreach (var asignacion in asignaciones)
            {
                columna.Item().Element(c => ConstruirSeccionAsignacion(c, asignacion, numeroOperador++));
            }

            ConstruirTotalesGenerales(columna.Item(), asignaciones);
        });
    }

    private void ConstruirSeccionAsignacion(IContainer contenedor, Asignacion asignacion, int numeroOperador)
    {
        var choferNombre = asignacion.Chofer != null
            ? $"{asignacion.Chofer.Nombre} {asignacion.Chofer.ApellidoPaterno}".Trim().ToUpper()
            : "SIN ASIGNAR";

        var origen = asignacion.SucursalOrigen?.Nombre.ToUpper() ?? "ORIGEN";
        var destino = asignacion.SucursalDestino?.Nombre.ToUpper() ?? "DESTINO";

        contenedor.PaddingBottom(10).Column(col =>
        {
            col.Item().Background(Colors.Grey.Lighten3).Padding(3).Row(r =>
            {
                r.RelativeItem().Text($"Operador N {numeroOperador}   {origen} -> {destino}   |   CHOFER: {choferNombre}")
                    .FontSize(8.5f).Bold();
            });

            col.Item().Table(tabla =>
            {
                tabla.ColumnsDefinition(columnas =>
                {
                    columnas.ConstantColumn(50);  // Folio / Clave
                    columnas.ConstantColumn(75);  // Forma Pago
                    columnas.RelativeColumn(2);   // Remitente
                    columnas.RelativeColumn(2);   // Destinatario
                    columnas.ConstantColumn(35);  // Bultos
                    columnas.ConstantColumn(45);  // Seguro
                    columnas.ConstantColumn(50);  // SubTotal
                    columnas.ConstantColumn(45);  // ValorD
                    columnas.ConstantColumn(50);  // Flete
                    columnas.ConstantColumn(40);  // Peso
                    columnas.ConstantColumn(50);  // Total
                    columnas.ConstantColumn(70);  // Status
                });

                tabla.Header(encabezado =>
                {
                    encabezado.Cell().Text("Folio").Bold();
                    encabezado.Cell().Text("Prepago").Bold();
                    encabezado.Cell().Text("Remit.").Bold();
                    encabezado.Cell().Text("Destinatario").Bold();
                    encabezado.Cell().AlignRight().Text("Bultos").Bold();
                    encabezado.Cell().AlignRight().Text("Seguro").Bold();
                    encabezado.Cell().AlignRight().Text("SubTota").Bold();
                    encabezado.Cell().AlignRight().Text("ValorD").Bold();
                    encabezado.Cell().AlignRight().Text("Flete").Bold();
                    encabezado.Cell().AlignRight().Text("Peso").Bold();
                    encabezado.Cell().AlignRight().Text("Total").Bold();
                    encabezado.Cell().Text("Status").Bold();
                });

                var guiasPorDestino = asignacion.Guias.GroupBy(g => g.SucursalDestino?.Nombre.ToUpper() ?? "DESTINO NO ESPECIFICADO");

                foreach (var grupoDestino in guiasPorDestino)
                {
                    tabla.Cell().ColumnSpan(12).PaddingTop(2).Text($"Destino {grupoDestino.Key}")
                        .Bold().FontSize(8).FontColor(Colors.Blue.Darken3);

                    foreach (var guia in grupoDestino)
                    {
                        int bultos = guia.ArticulosGuia?.Sum(a => a.Cantidad) ?? 0;
                        decimal pesoTotal = guia.ArticulosGuia?.Sum(a => a.PesoTotalKg) ?? 0;
                        string pagoStr = FormatearFormaPago(guia.FormaPago);

                        tabla.Cell().Text(guia.Clave);
                        tabla.Cell().Text(pagoStr);
                        tabla.Cell().Text(guia.ClienteOrigen?.Nombre.ToUpper() ?? "-").ClampLines(1);
                        tabla.Cell().Text(guia.ClienteDestino?.Nombre.ToUpper() ?? "-").ClampLines(1);
                        tabla.Cell().AlignRight().Text(bultos.ToString());
                        tabla.Cell().AlignRight().Text(guia.CobroSeguro.ToString("N2"));
                        tabla.Cell().AlignRight().Text(guia.Subtotal.ToString("N2"));
                        tabla.Cell().AlignRight().Text("0.00");
                        tabla.Cell().AlignRight().Text(guia.Flete.ToString("N2"));
                        tabla.Cell().AlignRight().Text(pesoTotal.ToString("N2"));
                        tabla.Cell().AlignRight().Text(guia.Total.ToString("N2"));
                        tabla.Cell().Text($"({pagoStr})");
                    }
                }
            });

            var subtotal = asignacion.Guias.Sum(g => g.Subtotal);
            var flete = asignacion.Guias.Sum(g => g.Flete);
            var seguro = asignacion.Guias.Sum(g => g.CobroSeguro);
            var totalBultos = asignacion.Guias.Sum(g => g.ArticulosGuia?.Sum(a => a.Cantidad) ?? 0);
            var pesoTotalAsig = asignacion.Guias.Sum(g => g.ArticulosGuia?.Sum(a => a.PesoTotalKg) ?? 0);

            var pagadas = asignacion.Guias.Where(g => g.FormaPago == FormaPago.Pagado).Sum(g => g.Total);
            var cobdes = asignacion.Guias.Where(g => g.FormaPago == FormaPago.PorCobrarDestino).Sum(g => g.Total);
            var credito = asignacion.Guias.Where(g => g.FormaPago == FormaPago.CreditoOrigen).Sum(g => g.Total);
            var prepagadas = asignacion.Guias.Where(g => g.FormaPago == FormaPago.Prepagado).Sum(g => g.Total);

            col.Item().PaddingTop(3).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);

            col.Item().Row(r =>
            {
                r.RelativeItem(6).Column(c =>
                {
                    c.Item().Text($"Subtotales ============> Guías: {asignacion.Guias.Count} | Bultos: {totalBultos} | Subtotal: ${subtotal:N2} | Flete: ${flete:N2} | Seguro: ${seguro:N2} | Peso: {pesoTotalAsig:N2} kg").Bold();
                    c.Item().Text($"Subtotal Facturas pagadas =====> ${pagadas:N2}");
                    c.Item().Text($"Subtotal Facturas Cobrar Destino ==> ${cobdes:N2}");
                    c.Item().Text($"Subtotal Facturas Credito ======> ${credito:N2}");
                    c.Item().Text($"Subtotal Facturas Prepagadas ====> ${prepagadas:N2}");
                });

                r.RelativeItem(4).Column(c =>
                {
                    c.Item().Text("RESPONSABLE DE RUTA: ___________________________").FontSize(7);
                    c.Item().Text("TRANSBORDO RESPONSABLE: ____________________________").FontSize(7);
                });
            });

            col.Item().PaddingTop(2).Text("YO _______________________________ RESPONSABLE DE LA RUTA QUE VIAJA DE _______________________ A _________________________ PARA EL INTERCAMBIO, ME COMPROMETO A NO EXCEDER LOS LIMITES DE VELOCIDAD QUE ES DE 90 KM POR HORA. FIRMA _____________________________________________")
                .FontSize(6).Italic();
        });
    }

    private void ConstruirTotalesGenerales(IContainer contenedor, List<Asignacion> asignaciones)
    {
        var todasGuias = asignaciones.SelectMany(a => a.Guias).ToList();
        int totalBultos = todasGuias.Sum(g => g.ArticulosGuia?.Sum(a => a.Cantidad) ?? 0);
        decimal totalSubtotal = todasGuias.Sum(g => g.Subtotal);
        decimal totalFlete = todasGuias.Sum(g => g.Flete);
        decimal totalSeguro = todasGuias.Sum(g => g.CobroSeguro);
        decimal totalPeso = todasGuias.Sum(g => g.ArticulosGuia?.Sum(a => a.PesoTotalKg) ?? 0);
        decimal totalGral = todasGuias.Sum(g => g.Total);

        contenedor.BorderTop(1).BorderColor(Colors.Black).PaddingTop(4).Column(col =>
        {
            col.Item().Row(r =>
            {
                r.RelativeItem().Text($"Totales Generales ======> Asignaciones: {asignaciones.Count} | Guías: {todasGuias.Count} | Bultos: {totalBultos} | SubTotal: ${totalSubtotal:N2} | Flete: ${totalFlete:N2} | Seguro: ${totalSeguro:N2} | Peso: {totalPeso:N2} kg | Total: ${totalGral:N2}")
                    .Bold().FontSize(8);
            });
            col.Item().PaddingTop(2).Text("OBSERVACIONES / AUTORIZADO: ____________________________________________________________________________________").FontSize(7.5f);
        });
    }

    private void ConstruirPiePagina(IContainer contenedor)
    {
        contenedor.AlignCenter().Text(t =>
        {
            t.Span("Página ");
            t.CurrentPageNumber();
            t.Span(" de ");
            t.TotalPages();
        });
    }

    private static string FormatearFormaPago(FormaPago formaPago)
    {
        return formaPago switch
        {
            FormaPago.Pagado => "PAGADO",
            FormaPago.Prepagado => "PREPAGADA",
            FormaPago.PorCobrarDestino => "COBDES",
            FormaPago.CreditoOrigen => "CRED ORIGEN",
            _ => formaPago.ToString().ToUpper()
        };
    }
}