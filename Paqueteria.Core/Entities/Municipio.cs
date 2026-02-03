using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("municipios")]
public class Municipio
{
    [Key]
    [Column("id_municipio")]
    public Guid IdMunicipio { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(2)]
    [Column("estado")]
    public string IdEstado { get; set; } = string.Empty;

    // llaves
    [ForeignKey("IdEstado")]
    public virtual Estado Estado { get; set; } = null!;

    // colleciones
    public virtual ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();
}