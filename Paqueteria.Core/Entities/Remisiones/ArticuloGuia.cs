using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Sat;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("articulos_guia", Schema = Constantes.Esquemas.Remisiones)]
public class ArticuloGuia
{
    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; } = Guid.NewGuid();
        
        [Column("descipcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("guia_id")]
        public Guid GuiaId { get; init; }

        [Column("articulo_id")]
        public string ArticuloId { get; init; } = string.Empty;

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("peso_unidad", TypeName = "decimal(10,2)")]
        public decimal PesoUnidad { get; set; }

        [Column("valor_unidad", TypeName = "decimal(10,2)")]
        public decimal ValorUnidad { get; set; }

    #endregion

    #region ForeignKeys

        [ForeignKey("GuiaId")]
        public Guia Guia { get; init; } = null!;

        [ForeignKey("ArticuloId")]
        public Articulo Articulo { get; init; } = null!;

    #endregion
    
    #region Constructors
        
        private ArticuloGuia() {}

        public static ArticuloGuia Create
        (
            Guid guiaId,
            string descripcion,
            string articuloId,
            int cantidad,
            decimal pesoUnidad,
            decimal valorUnidad
        )
        {
            //TODO Validar campos

            return new ArticuloGuia()
            {
                Id = Guid.NewGuid(),
                Descripcion = descripcion,
                GuiaId =  guiaId,
                ArticuloId =  articuloId,
                Cantidad =  cantidad,
                PesoUnidad = pesoUnidad,
                ValorUnidad = valorUnidad
            };
        }
    
    #endregion
}