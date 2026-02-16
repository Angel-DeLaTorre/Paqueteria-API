using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("articulos_guia")]
public class ArticuloGuia
{
    [Key]
    [Column("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Column("guia_id")]
    public Guid GuiaId { get; private set; }

    [Column("articulo_id")]
    public Guid ArticuloId { get; private set; }

    [Column("cantidad")]
    public int Cantidad { get; private set; }

    [Column("peso", TypeName = "decimal(10,2)")]
    public decimal Peso { get; private set; }

    // llaves
    [ForeignKey("GuiaId")]
    public virtual Guia Guia { get; private set; } = null!;

    [ForeignKey("ArticuloId")]
    public virtual Articulo Articulo { get; private set; } = null!;
}