using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Remisiones
{
    [Table("asignaciones", Schema = Constantes.Esquemas.Remisiones)]
    public class Asignacion
    {
        #region Columns
        
            [Key]
            [Column("id")]
            public Guid Id { get; init; } = Guid.NewGuid();
            
            [Column("guia_id")]
            public Guid GuiaId { get; init; }

            [Column("chofer_id")]
            public Guid ChoferId { get; init; }

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
            public string Camion { get; set; } =  string.Empty;

            [MaxLength(50)]
            [Column("num_contenedor")]
            public string? NumContenedor { get; set; }

            [MaxLength(50)]
            [Column("num_contenedor2")]
            public string? NumContenedor2 { get; set; }

        #endregion

        #region ForeignKey

            [ForeignKey("ChoferId")]
            public Chofer Chofer { get; init; } = null!;

            [ForeignKey("GuiaId")] 
            public Guia Guia { get; init; } = null!;

            #endregion
    }
}