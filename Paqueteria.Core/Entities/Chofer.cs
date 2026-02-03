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
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("apellido_paterno")]
        public string ApellidoPaterno { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("apellido_materno")]
        public string? ApellidoMaterno { get; set; }

        [Column("estatus_generico")]
        public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

        [MaxLength(200)]
        [Column("direccion")]
        public string? Direccion { get; set; }

        [MaxLength(20)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("num_camion")]
        public int NumCamion { get; set; }

        [Column("num_contenedor")]
        public int NumContenedor { get; set; }

        [Column("num_contenedor2")]
        public int NumContenedor2 { get; set; }

        [Column("fecha_alta")]
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        [Column("fecha_baja")]
        public DateTime? FechaBaja { get; set; }

        // Navegacion
        public virtual ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
    }
}