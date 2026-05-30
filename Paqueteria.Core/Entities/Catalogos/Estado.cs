using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Catalogos;

[Table("estados", Schema = Constantes.Esquemas.Catalogos)]
public class Estado
{
    
    #region Columns
    
        [Key]
        [MaxLength(3)]
        [Column("id")]
        public string Id { get; init; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; init; } = string.Empty;
        
        [Required]
        [MaxLength(3)]
        [Column("pais")]
        public string Pais { get; init; } = string.Empty;

        [Required]
        [MaxLength(2)]
        [Column("acronimo_2")]
        public string Acronimo2 { get; init; } = string.Empty;
    
    #endregion
}