using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("estados")]
public class Estado
{
    [Key]
    [MaxLength(3)]
    [Column("id")]
    public string Id { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("nombre")]
    public string Nombre { get; init; } = string.Empty;

    [Required]
    [MaxLength(2)]
    [Column("acronimo_2")]
    public string Acronimo2 { get; init; } = string.Empty;
}