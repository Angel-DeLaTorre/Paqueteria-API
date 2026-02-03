using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("articulos_guia")]
public class ArticuloGuia
{
    [Key]
    [Column("id_articulos_guia")]
    public Guid IdArticuloGuia { get; set; } = Guid.NewGuid();

    [Column("id_guia")]
    public Guid IdGuia { get; set; }

    [Column("id_articulo")]
    public Guid IdArticulo { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("peso", TypeName = "decimal(10,2)")]
    public decimal Peso { get; set; }

    // llaves
    [ForeignKey("IdGuia")]
    public virtual Guia Guia { get; set; } = null!;

    [ForeignKey("IdArticulo")]
    public virtual Articulo Articulo { get; set; } = null!;
}