using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("direcciones_guia_snapshot")]
public class DireccionGuiaSnapshot
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(200)]
    [Column("calle")]
    public string Calle { get; set; }  = string.Empty;

    [MaxLength(20)]
    [Column("numero_exterior")]
    public string NumeroExterior { get; set; }  = string.Empty;

    [MaxLength(20)]
    [Column("numero_interior")]
    public string? NumeroInterior { get; set; }

    [MaxLength(100)]
    [Column("colonia")]
    public string Colonia { get; set; } = string.Empty;

    [MaxLength(6)]
    [Column("codigo_postal")]
    public string CodigoPostal { get; set; }  = string.Empty;

    [MaxLength(100)]
    [Column("localidad")]
    public string? Localidad { get; set; }

    [Column("municipio_id")]
    public Guid MunicipioId { get; set; }

    // llaves
    [ForeignKey("IdMunicipio")]
    public virtual Municipio Municipio { get; set; } = null!;
}