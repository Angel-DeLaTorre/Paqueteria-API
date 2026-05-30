using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Sat;

[Table("articulos", Schema = Constantes.Esquemas.Sat)]
public class Articulo
{
    #region Columns

        [Key]
        [Column("id")]
        public string Id { get; init; } =  string.Empty;

        [Column("texto")]
        public string Texto { get; set; } =  string.Empty;
        
        [Column("similares")]
        public string Similares { get; set; } =  string.Empty;
        
        [Column("material_peligroso")]
        public string MaterialPeligroso { get; set; } =  string.Empty;
        
        [Column("vigencia_desde")]
        public DateTime VigenciaDesde { get; set; }
        
        [Column("vigencia_hasta")]
        public DateTime VigenciaHasta { get; set; }
        
        [MaxLength(50)]
        [Column("clave")]
        public string Clave { get; set; } = string.Empty;

    #endregion
    
    #region Constructors
    
        private Articulo() { }

        public static Articulo Create(string id, string texto, string similares, string materialPeligroso, DateTime vigenciaDesde, DateTime vigenciaHasta)
        {
            //TODO Validar campos
            
            return new Articulo
            {
                Id = id,
                Texto = texto,
                Similares = similares,
                MaterialPeligroso = materialPeligroso,
                VigenciaDesde = vigenciaDesde,
                VigenciaHasta = vigenciaHasta
            };
        }
        
    #endregion
}