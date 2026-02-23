using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("articulos")]
public class Articulo
{
    [Key]
    [Column("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    [Column("clave")]
    public string Clave { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("descripcion")]
    public string Descripcion { get; set; } = string.Empty;

    [Column("estatus")]
    public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

    public Articulo (){}

    public Articulo(string clave, string descripcion)
    {
        Clave = clave;
        Descripcion = descripcion;
    }
    public Articulo(string clave, string descripcion,  EstatusGenerico estatus)
    {
        Clave = clave;
        Descripcion = descripcion;
        Estatus = estatus;
    }
}