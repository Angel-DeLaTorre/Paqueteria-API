using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("rutas")]
public class Ruta
{
    #region Campos
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("sucursal_origen_id")]
        public Guid SucursalOrigenId { get; set; }

        [Column("sucursal_destino_id")]
        public Guid SucursalDestinoId { get; set; }

        [MaxLength(200)]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("num_camion")]
        public int? NumCamion { get; set; }

        [Column("num_contenedor")]
        public int? NumContenedor { get; set; }

        [Column("num_contenedor_2")]
        public int? NumContenedor2 { get; set; }
    #endregion

    #region Llaves
        [ForeignKey("SucursalOrigenId")]
        public virtual Sucursal SucursalOrigen { get; set; } = null!;

        [ForeignKey("SucursalDestinoId")]
        public virtual Sucursal SucursalDestino { get; set; } = null!;
    #endregion
}