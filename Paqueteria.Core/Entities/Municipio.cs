using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("municipios")]
public class Municipio
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(2)]
    [Column("estado")]
    public string EstadoId { get; set; } = string.Empty;

    // llaves
    [ForeignKey("EstadoId")]
    public Estado Estado { get; set; } = null!;

    // colleciones
    public ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();
}