using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("guias")]
public class Guia
{
    [Key]
    [Column("id_guia")]
    public Guid IdGuia { get; set; } = Guid.NewGuid();

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
    [Column("cliente_origen")]
    public Guid IdClienteOrigen { get; set; }

    [Column("direccion_origen")]
    public Guid IdDireccionOrigen { get; set; }

    // Destino
    [Column("cliente_destino")]
    public Guid IdClienteDestino { get; set; }

    [Column("direccion_destino")]
    public Guid IdDireccionDestino { get; set; }

    // Sucursales involucradas
    [Column("sucursal_origen")]
    public Guid IdSucursalOrigen { get; set; }

    [Column("sucursal_destino")]
    public Guid IdSucursalDestino { get; set; }

    // Usuarios responsables
    [Column("usuario_alta")]
    public Guid? IdUsuarioAlta { get; set; }

    [Column("usuario_cobro")]
    public Guid? IdUsuarioCobro { get; set; }

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

    // llaves
    [ForeignKey("IdClienteOrigen")]
    public virtual Cliente ClienteOrigen { get; set; } = null!;

    [ForeignKey("IdClienteDestino")]
    public virtual Cliente ClienteDestino { get; set; } = null!;

    [ForeignKey("IdDireccionOrigen")]
    public virtual DireccionGuiaSnapshot DireccionOrigen { get; set; } = null!;

    [ForeignKey("IdUsuarioAlta")]
    public virtual Usuario? UsuarioAlta { get; set; }

    [ForeignKey("IdUsuarioCobro")]
    public virtual Usuario? UsuarioCobro { get; set; }

    [ForeignKey("IdDireccionDestino")]
    public virtual DireccionGuiaSnapshot DireccionDestino { get; set; } = null!;

    [ForeignKey("IdSucursalOrigen")]
    public virtual Sucursal SucursalOrigen { get; set; } = null!;

    [ForeignKey("IdSucursalDestino")]
    public virtual Sucursal SucursalDestino { get; set; } = null!;

    // colleciones
    public virtual ICollection<ArticuloGuia> Articulos { get; set; } = new List<ArticuloGuia>();

    public virtual ICollection<AsignacionGuia> Asignaciones { get; set; } = new List<AsignacionGuia>();
}