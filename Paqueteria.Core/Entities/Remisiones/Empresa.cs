using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("empresas", Schema = Constantes.Esquemas.Remisiones)]
public class Empresa
{
    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(150)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("nombre_corto")]
        public string? NombreCorto { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("rfc")]
        public string Rfc { get; set; } = string.Empty;

        public Direccion Direccion { get; set; } = null!;

        [Column("fecha_alta")]
        public DateTime FechaAlta { get; init; }

    #endregion

    #region ForeignKeys
        
        [ForeignKey("Direccion.MunicipioId")] public virtual Municipio Municipio { get; set; } = null!;
        
    #endregion
    
    #region Constructors
    
        private Empresa() { }

        public static Empresa Create(string nombre, string nombreCorto, string rfc, Direccion direccion)
        {
            //TODO Validar campos
            
            return new Empresa()
            {
                Id = Guid.NewGuid(),
                Nombre = nombre,
                NombreCorto = nombreCorto,
                Rfc = rfc,
                Direccion = direccion,
                FechaAlta = DateTime.UtcNow
            };
        }
    
    #endregion
}