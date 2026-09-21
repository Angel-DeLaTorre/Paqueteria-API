using Paqueteria.Aplicacion.Comun.Mapeador;
using Paqueteria.Aplicacion.Modulos.Clientes;
using Paqueteria.Comun.Dtos.Guias;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Aplicacion.Modulos.Guias.Mapeador;

public static class GuiaMapeador
{
    
    extension(Guia guia)
    {
        public GuiaRespuestaDto MapeaRespuestaDto()
        {
            var clienteOrigen = guia.ClienteOrigen.MapeaRespuestaDto();
            var direccionOrigen = guia.DireccionOrigen.MapeaRespuestaDto();
            var clienteDestino = guia.ClienteDestino.MapeaRespuestaDto();
            var direccionDestino = guia.DireccionDestino.MapeaRespuestaDto();
            // Protege contra colecciones nulas
            var articulos = guia.ArticulosGuia?.Select(a => a.MapeaRespuestaDto()) ?? [];
    
            return new GuiaRespuestaDto(
                guia.Id,
                guia.Clave,
                guia.FormaPago,
                guia.FechaEnvio,
                guia.FechaPago,
                guia.ClienteOrigenId,
                clienteOrigen,
                direccionOrigen,
                guia.SucursalOrigenId,
                guia.SucursalOrigen?.Nombre ?? string.Empty,
                clienteDestino,
                direccionDestino,
                guia.ClienteDestinoId,
                guia.SucursalDestinoId,
                guia.SucursalDestino?.Nombre ?? string.Empty,
                guia.UsuarioCobroId,
                guia.UsuarioCobro?.Nombre ?? string.Empty,
                guia.Flete,
                guia.CobroSeguro,
                guia.Recoleccion,
                guia.EntregaA,
                guia.Maniobras,
                guia.Peaje,
                guia.Lineas,
                guia.CondonaIva,
                guia.Iva,
                guia.IvaRetenido,
                guia.Subtotal,
                guia.Total,
                guia.ImporteTexto,
                guia.Observaciones,
                guia.EstaAsegurado,
                guia.SeguroId,
                guia.PolizaSeguro,
                articulos
            );
        } 
        
        public EtiquetaPaqueteRespuestaDto AEtiquetaDto()
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

        public RemisionPdfDto ToRemisionPdfDto()
        {
            ArgumentNullException.ThrowIfNull(guia);
        
            var articulos = guia.ArticulosGuia?.Select(art => new ItemArticuloRemisionDto(
                art.Cantidad,
                art.Descripcion,
                art.ValorUnidad,
                art.PesoUnitarioKg,
                art.Cantidad * art.ValorUnidad
            )).ToList() ?? [];

            return new RemisionPdfDto(
                ClaveGuia: guia.Clave,
                FechaCaptura: guia.FechaCaptura,
                FormaPago: guia.FormaPago.ToString(),
                Estatus: guia.Estatus.ToString(),

                // Origen
                OrigenCiudad: guia.SucursalOrigen?.Nombre ?? string.Empty,
                RemitenteNombre: guia.ClienteOrigen?.Nombre ?? string.Empty,
                RfcRemitente: guia.ClienteOrigen?.Rfc,
                DireccionOrigen: FormatearDireccion(guia.DireccionOrigen?.Direccion),
                TelefonoRemitente: guia.ClienteOrigen?.Telefono 
                                   ?? guia.ClienteOrigen?.Telefono2 
                                   ?? string.Empty,

                // Destino
                DestinoCiudad: guia.SucursalDestino?.Nombre ?? string.Empty,
                DestinatarioNombre: guia.ClienteDestino?.Nombre ?? string.Empty,
                RfcDestinatario: guia.ClienteDestino?.Rfc,
                DireccionDestino: FormatearDireccion(guia.DireccionDestino?.Direccion),
                TelefonoDestino: guia.ClienteDestino?.Telefono 
                                 ?? guia.ClienteDestino?.Telefono2 
                                 ?? string.Empty,

                // Artículos
                Articulos: articulos,

                // Desglose Financiero
                Flete: guia.Flete,
                Recoleccion: guia.Recoleccion,
                EntregaA: guia.EntregaA,
                Maniobras: guia.Maniobras,
                Peaje: guia.Peaje,
                Lineas: guia.Lineas,
                Subtotal: guia.Subtotal,
                Iva: guia.Iva,
                IvaRetenido: guia.IvaRetenido,
                Total: guia.Total,
                ImporteTexto: guia.ImporteTexto ?? string.Empty,

                // Información Adicional
                UsuarioCaptura: guia.UsuarioAlta?.Nombre 
                                ?? guia.UsuarioAlta?.Nombre 
                                ?? string.Empty,
                Observaciones: guia.Observaciones
            );
        }
    }

    private static string FormatearDireccion(Direccion? direccion)
    {
        if (direccion is null) 
            return string.Empty;

        var partes = new List<string>();
        
        if (!string.IsNullOrWhiteSpace(direccion.Calle))
        {
            var numExt = !string.IsNullOrWhiteSpace(direccion.NumeroExterior) ? $"#{direccion.NumeroExterior}" : string.Empty;
            partes.Add($"{direccion.Calle} {numExt}".Trim());
        }
        if (!string.IsNullOrWhiteSpace(direccion.NumeroInterior))
            partes.Add($"INT. {direccion.NumeroInterior}");
        
        if (!string.IsNullOrWhiteSpace(direccion.Colonia))
            partes.Add($"COL. {direccion.Colonia}");
        
        if (!string.IsNullOrWhiteSpace(direccion.CodigoPostal))
            partes.Add($"C.P. {direccion.CodigoPostal}");

        return string.Join(", ", partes);
    }

    private static DireccionGuiaSnapRespuestaDto MapeaRespuestaDto(this DireccionGuiaSnapshot entidad)
    {
        return new DireccionGuiaSnapRespuestaDto(
            entidad.Id,
            entidad.Direccion.MapeaRespuestaDto(),
            entidad.Direccion.Municipio.Nombre
        );
    }

    

}