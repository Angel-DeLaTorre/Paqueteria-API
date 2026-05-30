using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Catalogos;

[Table("municipios", Schema = Constantes.Esquemas.Catalogos)]
public class Municipio
{
    #region Columns
    
        [Key]
        [Required]
        [Column("id")]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(3)]
        [Column("sat_id")]
        public string SatId { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        [Column("estado")]
        public string EstadoId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(70)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;
    
    #endregion

    #region ForeignKey
        [ForeignKey("EstadoId")] public Estado Estado { get; init; } = null!;
    #endregion
}