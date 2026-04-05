using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("direcciones_cliente", Schema = Constantes.Esquemas.Remisiones)]
public class DireccionCliente
{
    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; } = Guid.NewGuid();

        public Direccion Direccion { get; set; } = null!;

    #endregion

    #region ForeignKey

        [ForeignKey("Direccion.MunicipioId")] public virtual Municipio Municipio { get; set; } = null!;

    #endregion
    
    #region Constructors
    
        private DireccionCliente () {}

        public static DireccionCliente Create(Direccion direccion)
        {
            //TODO Validar campos

            return new DireccionCliente()
            {
                Id = Guid.NewGuid(),
                Direccion = direccion,
            };
        }
    
    #endregion
}