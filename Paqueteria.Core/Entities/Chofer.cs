using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities
{
    [Table("choferes")]
    public class Chofer
    {
        [Key]
        [Column("chofer_id")]
        public Guid ChoferId { get; init; } = Guid.NewGuid();

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

        [Column("estatus")]
        public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

        [Column("calle")]
        [MaxLength(100)]
        public string? Calle { get; set; } = string.Empty;

        [Column("colonia")]
        [MaxLength(100)]
        public string? Colonia { get; set; } = string.Empty;

        [Column("numero_exterior")]
        [MaxLength(10)]
        public string? NumeroExterior { get; set; }  = string.Empty;

        [Column("numero_interior")]
        [MaxLength(10)]
        public string? NumeroInterior { get; set; }

        [Column("localidad")]
        [MaxLength(250)]
        public string? Localidad { get; set; }

        [Column("id_municipio")]
        public Guid IdMunicipio { get; private set; }

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
        public DateTime FechaAlta { get; init; } = DateTime.UtcNow;

        [Column("fecha_baja")]
        public DateTime? FechaBaja { get; set; }

        // llaves
        [ForeignKey("IdMunicipio")]
        public virtual Municipio Municipio { get; private set; } = null!;

        // Navegacion
        public virtual ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
    }
}