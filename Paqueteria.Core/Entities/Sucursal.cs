using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("sucursales")]
public class Sucursal
{
    [Key]
    [Column("id_sucursal")]
    public Guid IdSucursal { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Codigo { get; set; } = string.Empty;

    [Column("es_matriz")]
    public bool EsMatriz { get; set; } = false;

    [Column("id_municipio")]
    public Guid IdMunicipio { get; set; }

    [Column("direccion")]
    [MaxLength(250)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Column("estatus")]
    public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

    [Column("ip_servidor_local")]
    [MaxLength(50)]
    public string? IpServidorLocal { get; set; }

    // llaves
    [ForeignKey("IdMunicipio")]
    public virtual Municipio Municipio { get; set; } = null!;

    // colleciones
    [InverseProperty("SucursalOrigen")]
    public virtual ICollection<Ruta> RutasOrigen { get; set; } = new List<Ruta>();

    [InverseProperty("SucursalDestino")]
    public virtual ICollection<Ruta> RutasDestino { get; set; } = new List<Ruta>();
}