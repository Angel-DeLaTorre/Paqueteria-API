using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Utilerias;

namespace Paqueteria.Dominio.Entidades.Remisiones;

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
    
    /// <summary>
    /// Sube la guía al vehículo de transporte (Salida / Carga).
    /// </summary>
    public void AsignarATransporte(Guid asignacionId)
    {
        if (asignacionId == Guid.Empty)
            throw new ArgumentException("El ID de asignación no es válido.", nameof(asignacionId));

        AsignacionId = asignacionId;
        Estatus = EstatusGuia.EnTransito;
    }
    
    /// <summary>
    /// Registra la bajada/ingreso de la guía a una bodega física (Descarga / Arribo).
    /// </summary>
    public void ArrivoASucursal(Guid nuevaSucursalId)
    {
        if (nuevaSucursalId == Guid.Empty)
            throw new ArgumentException("El ID de sucursal no es válido.", nameof(nuevaSucursalId));

        SucursalActualId = nuevaSucursalId;
        AsignacionId = null;
        
        if (nuevaSucursalId == SucursalOrigenId)
        {
            Estatus = EstatusGuia.EnBodegaOrigen;
        }
        else if (nuevaSucursalId == SucursalDestinoId)
        {
            Estatus = EstatusGuia.EnBodegaDestino;
        }
        else
        {
            Estatus = EstatusGuia.EnBodegaTransbordo;
        }
    }
    
    /// <summary>
    /// Marca la guía como entregada al cliente final.
    /// </summary>
    public void RegistrarEntrega()
    {
        AsignacionId = null;
        Estatus = EstatusGuia.Entregada;
    }

    public void Desasignar()
    {
        AsignacionId = null;
    }
}