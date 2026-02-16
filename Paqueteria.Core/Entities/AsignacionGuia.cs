using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Entities;

namespace Paqueteria.Core.Entities
{
    [Table("asignaciones_guias")]
    public class AsignacionGuia
    {
        [Key]
        [Column("id")]
        public Guid IdAsignacionGuia { get; set; } = Guid.NewGuid();

        [Column("asignacion_id")]
        public Guid AsignacionId { get; set; }

        [Column("guia_id")]
        public Guid GuiaId { get; set; }

        // llaves
        [ForeignKey("AsignacionId")]
        public virtual Asignacion Asignacion { get; set; } = null!;

        [ForeignKey("GuiaId")]
        public virtual Guia Guia { get; set; } = null!;
    }
}