using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("guias")]
public class Guia
{
    [Key]
    [Column("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [MaxLength(20)]
    [Column("clave")]
    public string Clave { get; set; } = string.Empty;

    [Column("forma_pago")]
    public FormaPago FormaPago { get; set; }

    [Column("fecha_captura")]
    public DateTime FechaCaptura { get; set; } = DateTime.UtcNow;

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
    public Guid UsuarioAltaId { get; set; }

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

    #region Llaves foraneas

        [ForeignKey("ClienteOrigenId")]
        public virtual Cliente ClienteOrigen { get; set; } = null!;

        [ForeignKey("ClienteDestinoId")]
        public virtual Cliente ClienteDestino { get; set; } = null!;

        [ForeignKey("DireccionOrigenId")]
        public virtual DireccionGuiaSnapshot DireccionOrigen { get; set; } = null!;

        [ForeignKey("UsuarioAltaId")]
        public virtual Usuario UsuarioAlta { get; set; } = null!;

        [ForeignKey("UsuarioCobroId")]
        public virtual Usuario? UsuarioCobro { get; set; }

        [ForeignKey("DireccionDestinoId")]
        public virtual DireccionGuiaSnapshot DireccionDestino { get; set; } = null!;

        [ForeignKey("SucursalOrigenId")]
        public virtual Sucursal SucursalOrigen { get; set; } = null!;

        [ForeignKey("SucursalDestinoId")]
        public virtual Sucursal SucursalDestino { get; set; } = null!;

    #endregion


}