using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("estados")]
public class Estado
{
    [Key]
    [MaxLength(2)]
    [Column("id_estado")]
    public string IdEstado { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(3)]
    [Column("acronimo_3")]
    public string? Acronimo3 { get; set; }

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}