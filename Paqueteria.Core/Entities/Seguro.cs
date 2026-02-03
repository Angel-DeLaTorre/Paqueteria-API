using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("seguros")]
public class Seguro
{
    [Key]
    [Column("id_seguro")]
    public Guid IdSeguro { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;
}