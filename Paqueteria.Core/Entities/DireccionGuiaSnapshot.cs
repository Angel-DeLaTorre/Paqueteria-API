using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("direcciones_guia_snapshot")]
public class DireccionGuiaSnapshot
{
    [Key]
    [Column("id_direccion")]
    public Guid IdDireccion { get; set; } = Guid.NewGuid();

    [MaxLength(200)]
    public string? Calle { get; set; }

    [MaxLength(20)]
    [Column("numero_exterior")]
    public string? NumeroExterior { get; set; }

    [MaxLength(20)]
    [Column("numero_interior")]
    public string? NumeroInterior { get; set; }

    [MaxLength(100)]
    public string? Colonia { get; set; }

    [MaxLength(6)]
    [Column("codigo_postal")]
    public string? CodigoPostal { get; set; }

    [MaxLength(100)]
    public string? Localidad { get; set; }

    [Column("id_municipio")]
    public Guid? IdMunicipio { get; set; }

    // llaves

    [ForeignKey("IdMunicipio")]
    public virtual Municipio? Municipio { get; set; }
}