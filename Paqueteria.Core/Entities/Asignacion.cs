using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities
{
    [Table("asignaciones")]
    public class Asignacion
    {
        #region Campos
            [Key]
            [Column("id")]
            public Guid Id { get; set; } = Guid.NewGuid();

            [Column("chofer_id")]
            public Guid ChoferId { get; set; }

            [Column("fecha_asignacion")]
            public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

            [MaxLength(10)]
            [Column("st1")]
            public string? St1 { get; set; }

            [MaxLength(10)]
            [Column("st2")]
            public string? St2 { get; set; }

            [MaxLength(10)]
            [Column("st3")]
            public string? St3 { get; set; }

            [MaxLength(10)]
            [Column("st4")]
            public string? St4 { get; set; }

            [Column("camion")]
            public int Camion { get; set; }

            [MaxLength(50)]
            [Column("num_contenedor")]
            public string? NumContenedor { get; set; }

            [MaxLength(50)]
            [Column("num_contenedor2")]
            public string? NumContenedor2 { get; set; }
        #endregion

        #region Llaves foraneas

        [ForeignKey("IdChofer")]
        public virtual Chofer Chofer { get; set; } = null!;

        #endregion
    }
}