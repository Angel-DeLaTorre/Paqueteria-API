using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("clientes")]
public class Cliente
{
    [Key]
    [Column("id_cliente")]
    public Guid IdCliente { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Column("estatus")]
    public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

    [MaxLength(20)]
    public string? Rfc { get; set; }

    [MaxLength(255)]
    public string? Direccion { get; set; }

    [MaxLength(255)]
    [Column("direccion_complemento")]
    public string? DireccionComplemento { get; set; }

    [MaxLength(6)]
    [Column("codigo_postal")]
    public string? CodigoPostal { get; set; }

    [MaxLength(100)]
    public string? Ciudad { get; set; }

    [MaxLength(20)]
    public string? Telefono { get; set; }

    [MaxLength(100)]
    public string? Correo { get; set; }

    [MaxLength(100)]
    public string? Contacto { get; set; }

    [MaxLength(50)]
    [Column("num_convenio")]
    public string? NumConvenio { get; set; }

    [MaxLength(50)]
    [Column("poliza_seguro")]
    public string? PolizaSeguro { get; set; }

    [Column("id_sucursal")]
    public Guid? IdSucursal { get; set; }

    [Column("fecha_alta")]
    public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

    // llaves
    [ForeignKey("IdSucursal")]
    public virtual Sucursal? Sucursal { get; set; }

    // collecions
    [InverseProperty("ClienteOrigen")]
    public virtual ICollection<Guia> GuiasEnviadas { get; set; } = new List<Guia>();

    [InverseProperty("ClienteDestino")]
    public virtual ICollection<Guia> GuiasRecibidas { get; set; } = new List<Guia>();
}