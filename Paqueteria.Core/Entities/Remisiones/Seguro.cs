using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("seguros", Schema = Constantes.Esquemas.Remisiones)]
public class Seguro
{
    #region Columns
    
        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;
        
        [Column("empresa_id")]
        public Guid EmpresaId { get; init; }
        
    #endregion
    
    #region Constructors
        
        private Seguro() {}

        public static Seguro Create(string nombre)
        {
            return new Seguro()
            {
                Id = Guid.NewGuid(),
                Nombre = nombre
            };
        }
    
    #endregion
}