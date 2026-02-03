using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("rutas")]
public class Ruta
{
    [Key]
    [Column("id_ruta")]
    public Guid IdRuta { get; set; } = Guid.NewGuid();

    [Column("sucursal_origen")]
    public Guid IdSucursalOrigen { get; set; }

    [Column("sucursal_destino")]
    public Guid IdSucursalDestino { get; set; }

    [MaxLength(200)]
    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("num_camion")]
    public int? NumCamion { get; set; }

    [Column("num_contenedor")]
    public int? NumContenedor { get; set; }

    [Column("num_contenedor_2")]
    public int? NumContenedor2 { get; set; }

    // llaves
    [ForeignKey("IdSucursalOrigen")]
    public virtual Sucursal SucursalOrigen { get; set; } = null!;

    [ForeignKey("IdSucursalDestino")]
    public virtual Sucursal SucursalDestino { get; set; } = null!;
}