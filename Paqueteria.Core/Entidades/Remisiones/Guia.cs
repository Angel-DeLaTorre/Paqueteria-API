using Paqueteria.Core.Entidades.Sistema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Remisiones;

public partial class Guia
{

    #region Columns

        public Guid Id { get; private init; } = Guid.CreateVersion7();
        public string Clave { get; private init; } = string.Empty;
        public EstatusGuia Estatus { get; private set; }
        public FormaPago FormaPago { get; private set; }
        public DateTime FechaCaptura { get; private init; }
        public DateTime? FechaEnvio { get; private set; }
        public DateTime? FechaPago { get; private set; }
        
        // Origen
        public Guid ClienteOrigenId { get; private set; }
        public Guid DireccionOrigenId { get; private set; }
        // Destino
        public Guid ClienteDestinoId { get; private set; }
        public Guid DireccionDestinoId { get; private set; }

        // Sucursales
        public Guid SucursalOrigenId { get; private set; }
        public Guid SucursalDestinoId { get; private set; }
        public Guid? SucursalActualId { get; private set; }

        // Usuarios responsables
        public Guid UsuarioAltaId { get; private init; }
        public Guid? UsuarioCobroId { get; private init; }

        // Datos Financieros
        public decimal Flete { get; private set; }
        public decimal Recoleccion { get; private set; }
        public decimal EntregaA { get; private set; }
        public decimal Maniobras { get; private set; }
        public decimal Peaje { get; private set; }
        public decimal Lineas { get; private set; }
        public bool CondonaIva { get; private set; }
        public decimal Iva { get; private set; }
        public decimal IvaRetenido { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal Total { get; set; }
        public decimal CobroSeguro { get; private set; }
        public string? ImporteTexto { get; private set; }
        
        public string? Observaciones { get; private set; }
        public bool EstaAsegurado { get; private set; }
        public string? PolizaSeguro { get; private set; }
        public Guid? SeguroId { get; private init; }
        public Guid? AsignacionId { get; internal set; }
        public Guid EmpresaId { get; private init; }
        

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
        public Sucursal? SucursalActual { get; private set; }
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