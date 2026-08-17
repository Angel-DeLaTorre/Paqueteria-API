using Paqueteria.Application.Modulos.Guias.Dtos;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Modulos.Guias.Mapeador;

public static class GuiaMapeador
{
    public static EtiquetaPaqueteRespuestaDto AEtiquetaDto(this Guia guia)
    {
        var direccionEntrega = "ENTREGA EN SUCURSAL";

        if (guia.DireccionDestino?.Direccion != null)
        {
            var dir = guia.DireccionDestino.Direccion;
            var numExt = !string.IsNullOrWhiteSpace(dir.NumeroExterior) ? $"#{dir.NumeroExterior}" : "";
            var colonia = !string.IsNullOrWhiteSpace(dir.Colonia) ? $"COL. {dir.Colonia}" : "";
            direccionEntrega = $"{dir.Calle} {numExt} {colonia}".Trim();
        }

        var articulosDto = guia.ArticulosGuia?.Select(a => new ArticuloEtiquetaDto(
            Cantidad: a.Cantidad,
            Descripcion: a.Descripcion
        )).ToList() ?? [];

        return new EtiquetaPaqueteRespuestaDto(
            ClaveGuia: guia.Clave,
            FolioInterno: guia.Clave,
            FechaCaptura: guia.FechaCaptura,
            FormaPago: guia.FormaPago.ToString(),

            // Remitente
            Origen: guia.SucursalOrigen?.Nombre ?? "N/A",
            Remitente: guia.ClienteOrigen?.Nombre ?? "N/A",
            DireccionOrigen: guia.DireccionOrigen?.Direccion != null 
                ? $"{guia.DireccionOrigen.Direccion.Calle} #{guia.DireccionOrigen.Direccion.NumeroExterior} COL. {guia.DireccionOrigen.Direccion.Colonia}" 
                : "N/A",
            RfcRemitente: guia.ClienteOrigen?.Rfc,
            TelefonoRemitente: guia.ClienteOrigen?.Telefono,

            // Destino
            Destino: guia.SucursalDestino?.Nombre ?? "N/A",
            Destinatario: guia.ClienteDestino?.Nombre ?? "N/A",
            DireccionDestino: direccionEntrega,
            RfcDestinatario: guia.ClienteDestino?.Rfc,
            TelefonoDestinatario: guia.ClienteDestino?.Telefono,

            // Contenido y Pie
            Articulos: articulosDto,
            Observaciones: guia.Observaciones,
            NumeroPaquete: 1,
            TotalPaquetes: guia.ArticulosGuia?.Count ?? 1
        );
    }
}