using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Entities;

namespace Paqueteria.Core.Entities
{
    [Table("asignaciones_guias")]
    public class AsignacionGuia
    {
        [Key]
        [Column("id_asignacion_guia")]
        public Guid IdAsignacionGuia { get; set; } = Guid.NewGuid();

        [Column("id_asignacion")]
        public Guid IdAsignacion { get; set; }

        [Column("id_guia")]
        public Guid IdGuia { get; set; }

        //Llaves
        [ForeignKey("IdAsignacion")]
        public virtual Asignacion Asignacion { get; set; } = null!;

        [ForeignKey("IdGuia")]
        public virtual Guia Guia { get; set; } = null!;
    }
}