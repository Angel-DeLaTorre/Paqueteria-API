using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("articulo")]
public class Articulo
{
    [Key]
    [Column("id_articulo")]
    public Guid IdArticulo { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Clave { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Descripcion { get; set; } = string.Empty;

    [Column("estatus")]
    public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;
}