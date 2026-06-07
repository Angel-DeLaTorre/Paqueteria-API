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

            [Column("chofer_id")]
            public Guid ChoferId { get; init; }

            [Column("fecha_creacion")]
            public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
            
            [Column("fecha_partida")]
            public DateTime? FechaPartida { get; set; } = DateTime.UtcNow;

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
            
            [Column("empresa_id")]
            public Guid EmpresaId { get; init; }

        #endregion

        #region ForeignKey

            [ForeignKey("ChoferId")]
            public Chofer Chofer { get; init; } = null!;
            
            [ForeignKey("EmpresaId")] public Empresa Empresa { get; init; } = null!;

        #endregion

        #region Constructos

            private Asignacion() { }

            public static Asignacion Create(
                Guid choferId, 
                DateTime? fechaPartida,
                string? st1, 
                string? st2, 
                string? st3, 
                string? st4, 
                string camion,
                string? numContenedor,
                string? numContenedor2)
            {
                return new Asignacion()
                {
                    Id = Guid.NewGuid(),
                    ChoferId = choferId,
                    FechaCreacion = DateTime.UtcNow,
                    FechaPartida = fechaPartida,
                    St1 = st1,
                    St2 = st2,
                    St3 = st3,
                    St4 = st4,
                    Camion = camion,
                    NumContenedor = numContenedor,
                    NumContenedor2 = numContenedor2
                };
            }

        #endregion
            
        #region Relations
        
            public ICollection<Guia> Guias { get; private set; } = new List<Guia>();
            
        #endregion
    }
}