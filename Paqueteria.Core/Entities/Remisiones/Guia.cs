using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Sistema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("guias", Schema = Constantes.Esquemas.Remisiones)]
public class Guia
{

    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; }
        
        [MaxLength(20)]
        [Column("clave")]
        public string Clave { get; init; } = string.Empty;

        [Column("estatus")]
        public EstatusGuia Estatus { get; set; }

        [Column("forma_pago")]
        public FormaPago FormaPago { get; set; }

        [Column("fecha_captura")]
        public DateTime FechaCaptura { get; init; }

        [Column("fecha_envio")]
        public DateTime? FechaEnvio { get; set; }

        [Column("fecha_pago")]
        public DateTime? FechaPago { get; set; }

        // Origen
        [Column("cliente_origen_id")]
        public Guid ClienteOrigenId { get; set; }

        [Column("direccion_origen_id")]
        public Guid DireccionOrigenId { get; set; }


        // Destino
        [Column("cliente_destino_id")]
        public Guid ClienteDestinoId { get; set; }

        [Column("direccion_destino_id")]
        public Guid DireccionDestinoId { get; set; }

        // Sucursales involucradas
        [Column("sucursal_origen_id")]
        public Guid SucursalOrigenId { get; set; }

        [Column("sucursal_destino_id")]
        public Guid SucursalDestinoId { get; set; }

        // Usuarios responsables
        [Column("usuario_alta_id")]
        public Guid UsuarioAltaId { get; init; }

        [Column("usuario_cobro_id")]
        public Guid? UsuarioCobroId { get; set; }

        // Datos Financieros
        [Column("costo_flete", TypeName = "decimal(15,2)")]
        public decimal CostoFlete { get; set; }

        [Column("iva", TypeName = "decimal(15,2)")]
        public decimal Iva { get; set; }

        [Column("ivar", TypeName = "decimal(15,2)")]
        public decimal IvaRetenido { get; set; }

        [Column("subtotal", TypeName = "decimal(15,2)")]
        public decimal Subtotal { get; set; }

        [Column("total", TypeName = "decimal(15,2)")]
        public decimal Total { get; set; }

        [Column("cobro_seguro", TypeName = "decimal(15,2)")]
        public decimal CobroSeguro { get; set; }

        // Datos Texto/Varios
        [MaxLength(200)]
        [Column("importe_texto")]
        public string? ImporteTexto { get; set; }

        [MaxLength(500)]
        [Column("observaciones")]
        public string? Observaciones { get; set; }

        [MaxLength(50)]
        [Column("poliza_seguro")]
        public string? PolizaSeguro { get; set; }

        [Column("seguro_id")]
        public Guid? SeguroId { get; init; }
        
        [Column("asignacion_id")]
        public Guid? AsignacionId { get; init; }
        
        [Column("empresa_id")]
        public Guid EmpresaId { get; init; }

    #endregion

    #region ForeignKeys

        [ForeignKey("UsuarioAltaId")]
        public Usuario UsuarioAlta { get; set; } = null!;

        [ForeignKey("UsuarioCobroId")]
        public Usuario? UsuarioCobro { get; set; }
    
        [ForeignKey("ClienteOrigenId")]
        public Cliente ClienteOrigen { get; set; } = null!;

        [ForeignKey("ClienteDestinoId")]
        public Cliente ClienteDestino { get; set; } = null!;
        
        [ForeignKey("DireccionOrigenId")]
        public DireccionGuiaSnapshot DireccionOrigen { get; set; } = null!;

        [ForeignKey("DireccionDestinoId")]
        public DireccionGuiaSnapshot DireccionDestino { get; set; } = null!;

        [ForeignKey("SucursalOrigenId")]
        public Sucursal SucursalOrigen { get; set; } = null!;

        [ForeignKey("SucursalDestinoId")]
        public Sucursal SucursalDestino { get; set; } = null!;
        
        [ForeignKey("AsignacionId")]
        public Asignacion? Asignacion { get; set; }

        [ForeignKey("SeguroId")]
        public Seguro Seguro { get; set; } = null!;
        
        [ForeignKey("EmpresaId")] 
        public Empresa Empresa { get; init; } = null!;

    #endregion
    
    #region Constructors

        private Guia() { }

        public static Guia Create(
            FormaPago formaPago, 
            DateTime? fechaPago,
            Guid clienteOrigenId,
            Guid direccionOrigenId,
            Guid clienteDestinoId,
            Guid direccionDestinoId,
            Guid sucursalOrigenId,
            Guid sucursalDestinoId,
            Guid usuarioAltaId,
            Guid? usuarioCobroId,
            decimal costoFlete,
            decimal iva,
            decimal ivaRetenido,
            decimal subtotal,
            decimal total,
            decimal cobroSeguro,
            string importeTexto,
            string? observaciones,
            string? polizaSeguro,
            Guid? seguroId,
            Guid empresaId
            
        )
        {
            return new Guia()
            {
                Id = Guid.NewGuid(),
                Estatus = EstatusGuia.Recolectado,
                FormaPago = formaPago,
                FechaPago = fechaPago,
                ClienteOrigenId = clienteOrigenId,
                DireccionOrigenId = direccionOrigenId,
                ClienteDestinoId = clienteDestinoId,
                DireccionDestinoId = direccionDestinoId,
                SucursalOrigenId = sucursalOrigenId,
                SucursalDestinoId = sucursalDestinoId,
                UsuarioAltaId = usuarioAltaId,
                UsuarioCobroId = usuarioCobroId,
                CostoFlete = costoFlete,
                Iva = iva,
                IvaRetenido = ivaRetenido,
                Subtotal = subtotal,
                Total = total,
                CobroSeguro = cobroSeguro,
                ImporteTexto = importeTexto,
                Observaciones = observaciones,
                PolizaSeguro = polizaSeguro,
                SeguroId = seguroId,
                EmpresaId = empresaId
            };
        }
    
    #endregion

}