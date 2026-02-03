using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("articulos_guia")]
public class ArticuloGuia
{
    [Key]
    [Column("id_articulos_guia")]
    public Guid IdArticuloGuia { get; set; } = Guid.NewGuid();

    [Column("id_remision")]
    public Guid IdGuiaCatalogo { get; set; }

    [Column("id_articulo")]
    public Guid IdArticuloCatalogo { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Peso { get; set; }

    //Llaves

    [ForeignKey("IdGuia")]
    public virtual Guia Guia { get; set; } = null!;

    [ForeignKey("IdArticuloCatalogo")]
    public virtual Articulo Articulo { get; set; } = null!;
}