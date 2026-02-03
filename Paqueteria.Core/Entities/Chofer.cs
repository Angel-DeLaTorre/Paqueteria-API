using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities
{
    [Table("choferes")]
    public class Chofer
    {
        [Key]
        [Column("id_chofere")]
        public Guid IdChofer { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ApellidoPaterno { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ApellidoMaterno { get; set; }

        public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        public int NumCamion { get; set; }
        public int NumContenedor { get; set; }
        public int NumContenedor2 { get; set; }

        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
        public DateTime? FechaBaja { get; set; }

        // Navegacion
        public virtual ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
    }
}