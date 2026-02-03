using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities
{
    [Table("asignaciones")]
    public class Asignacion
    {
        [Key]
        [Column("id_asignacion")]
        public Guid IdAsignacion { get; set; } = Guid.NewGuid();

        [Column("id_chofer")]
        public Guid IdChofer { get; set; }

        [ForeignKey("IdChofer")]
        public virtual Chofer Chofer { get; set; } = null!;

        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

        [MaxLength(10)]
        public string? St1 { get; set; }
        [MaxLength(10)]
        public string? St2 { get; set; }
        [MaxLength(10)]
        public string? St3 { get; set; }
        [MaxLength(10)]
        public string? St4 { get; set; }

        public int Camion { get; set; }

        [MaxLength(50)]
        public string? NumContenedor { get; set; }

        [MaxLength(50)]
        public string? NumContenedor2 { get; set; }

        public virtual ICollection<AsignacionGuia> DetallesGuias { get; set; } = new List<AsignacionGuia>();
    }
}