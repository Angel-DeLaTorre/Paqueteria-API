using Paqueteria.Core.Helpers;

namespace Paqueteria.Core.Entities.Remisiones;

public partial class Guia
{
    /// <summary>
    /// Agrega un artículo a la guía ejecutando las reglas de negocio del dominio.
    /// </summary>
    public void AgregarArticulo(ArticuloGuia articulo)
    {
        ArgumentNullException.ThrowIfNull(articulo);
        articulo.GuiaId = Id;
        ArticulosGuia.Add(articulo);
        CalcularCosto();
    }
    public void RemoverArticulo(ArticuloGuia articulo)
    {
        ArticulosGuia.Remove(articulo);
        CalcularCosto();
    }
    
    public void Pagar()
    {
        FechaPago = DateTime.Now;
    }
    
    public void ActualizarDireccionOrigen(DireccionGuiaSnapshot direccionOrigen, Guid clienteOrigenId)
    {
        ClienteOrigenId = clienteOrigenId;
        DireccionOrigen = direccionOrigen;
    }
    
    public void ActualizarDireccionDestino(DireccionGuiaSnapshot direccionDestino, Guid clienteDestinoId)
    {
        ClienteDestinoId = clienteDestinoId;
        DireccionDestino = direccionDestino;
    }

    public void ActualizarSucursalOrigen(Guid sucursalOrigenId)
    {
        SucursalOrigenId = sucursalOrigenId;
    }
    
    public void ActualizarSucursalDestino(Guid sucursalDestinoId)
    {
        SucursalDestinoId = sucursalDestinoId;
    }

    public void ActualizarObservaciones(string observaciones)
    {
        Observaciones = observaciones;
    }
    
    public void ActualizarPolizaSeguro(string polizaSeguro)
    {
        PolizaSeguro = polizaSeguro;
    }

    /// <summary>
    /// Recalcula los montos de la guia.
    /// </summary>
    private void CalcularCosto()
    {
        Subtotal = Flete + CobroSeguro + Recoleccion + EntregaA + Maniobras + Peaje + Lineas;
        Iva = CondonaIva ? 0m : (Subtotal * 0.16m);
        IvaRetenido = Subtotal * 0.04m;
        Total = (Subtotal + Iva) - IvaRetenido;
        ImporteTexto = NumeroALetrasHelper.Convertir(Total);
    }

    private void GenerarTextoImporte()
    {
        ImporteTexto = NumeroALetrasHelper.Convertir(Total);
    }

    public void Asignar(Guid asignacionId)
    {
        AsignacionId = asignacionId;
    }

    public void Desasignar()
    {
        AsignacionId = null;
    }
}