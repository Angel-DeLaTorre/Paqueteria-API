using Paqueteria.Core.Entities.Sistema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Remisiones;

public partial class Guia
{

    #region Columns

        public Guid Id { get; init; } = Guid.CreateVersion7();
        public string Clave { get; init; } = string.Empty;
        public EstatusGuia Estatus { get; private set; }
        public FormaPago FormaPago { get; set; }
        public DateTime FechaCaptura { get; init; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaPago { get; set; }
        
        // Origen
        public Guid ClienteOrigenId { get; set; }
        public Guid DireccionOrigenId { get; private set; }
        // Destino
        public Guid ClienteDestinoId { get; set; }
        public Guid DireccionDestinoId { get; private set; }

        // Sucursales
        public Guid SucursalOrigenId { get; set; }
        public Guid SucursalDestinoId { get; set; }

        // Usuarios responsables
        public Guid UsuarioAltaId { get; init; }
        public Guid? UsuarioCobroId { get; init; }

        // Datos Financieros
        public decimal Flete { get; set; }
        public decimal Recoleccion { get; set; }
        public decimal EntregaA { get; set; }
        public decimal Maniobras { get; set; }
        public decimal Peaje { get; set; }
        public decimal Lineas { get; set; }
        public bool CondonaIva { get; set; }
        public decimal Iva { get; set; }
        public decimal IvaRetenido { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public decimal CobroSeguro { get; set; }
        public string? ImporteTexto { get; set; }
        
        public string? Observaciones { get; set; }
        public bool EstaAsegurado { get; set; }
        public string? PolizaSeguro { get; set; }
        public Guid? SeguroId { get; init; }
        public Guid? AsignacionId { get; set; }
        public Guid EmpresaId { get; init; }
        

    #endregion

    #region ForeignKeys
        public Usuario UsuarioAlta { get; init; } = null!;
        public Usuario? UsuarioCobro { get; init; }
        public Cliente ClienteOrigen { get; private set; } = null!;
        public Cliente ClienteDestino { get; private set; } = null!;
        public DireccionGuiaSnapshot DireccionOrigen { get; set; } = null!;
        public DireccionGuiaSnapshot DireccionDestino { get; set; } = null!;
        public Sucursal SucursalOrigen { get; private set; } = null!;
        public Sucursal SucursalDestino { get; private set; } = null!;
        public Asignacion? Asignacion { get; private set; }
        public Seguro? Seguro { get; private set; }
        public Empresa Empresa { get; init; } = null!;
        public ICollection<ArticuloGuia> ArticulosGuia { get; private init; } = new List<ArticuloGuia>();
    #endregion
    
    #region Constructors

        private Guia() { }

        public static Guia Crear(
            string clave,
            FormaPago formaPago,
            DateTime? fechaPago,
            Guid clienteOrigenId,
            DireccionGuiaSnapshot direccionOrigenSnap,
            Guid clienteDestinoId,
            DireccionGuiaSnapshot direccionDestinoSnap,
            Guid sucursalOrigenId,
            Guid sucursalDestinoId,
            Guid usuarioAltaId,
            Guid? usuarioCobroId,
            decimal flete,
            decimal cobroSeguro,
            decimal recoleccion,
            decimal entregaA,
            decimal maniobras,
            decimal peaje,
            decimal lineas,
            decimal subtotal,
            decimal iva,
            decimal ivaRetenido,
            decimal total,
            bool condonaIva,
            bool estaAsegurado,
            string? observaciones,
            string? polizaSeguro,
            Guid? seguroId,
            Guid empresaId,
            List<ArticuloGuia>? articulos = null
        )
        {
            var guia = new Guia()
            {
                Clave = clave,
                Estatus = EstatusGuia.Recolectado,
                FormaPago = formaPago,
                FechaPago = fechaPago,
                FechaCaptura = DateTime.UtcNow, 
                FechaEnvio = DateTime.UtcNow,
                ClienteOrigenId = clienteOrigenId,
                DireccionOrigen = direccionOrigenSnap,
                ClienteDestinoId = clienteDestinoId,
                DireccionDestino = direccionDestinoSnap,
                SucursalOrigenId = sucursalOrigenId,
                SucursalDestinoId = sucursalDestinoId,
                UsuarioAltaId = usuarioAltaId,
                UsuarioCobroId = usuarioCobroId == Guid.Empty ? null : usuarioCobroId,
                Flete = flete,
                CobroSeguro = cobroSeguro,
                Recoleccion = recoleccion,
                EntregaA = entregaA,
                Maniobras = maniobras,
                Peaje = peaje,
                Lineas = lineas,
                Subtotal = subtotal,
                Iva = iva,
                IvaRetenido = ivaRetenido,
                Total = total,
                CondonaIva = condonaIva,
                EstaAsegurado = estaAsegurado,
                Observaciones = observaciones,
                PolizaSeguro = polizaSeguro,
                SeguroId = seguroId,
                EmpresaId = empresaId,
                ArticulosGuia = articulos ?? []
            };
            
            guia.GenerarTextoImporte();
            
            return guia;
        }
    
    #endregion

}