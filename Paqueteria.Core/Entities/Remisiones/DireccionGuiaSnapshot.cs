using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("direcciones_guia_snapshot", Schema = Constantes.Esquemas.Remisiones)]
public class DireccionGuiaSnapshot
{
    #region Column

        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        public Direccion Direccion { get; set; } = null!;

    #endregion

    #region ForeignKeys

        [ForeignKey("Direccion.MunicipioId")] public virtual Municipio Municipio { get; set; } = null!;

    #endregion
    
    #region Constructors
    
    private DireccionGuiaSnapshot () {}

    public static DireccionGuiaSnapshot Create(Direccion direccion)
    {
        //TODO Validar campos

        return new DireccionGuiaSnapshot()
        {
            Id = Guid.NewGuid(),
            Direccion = direccion,
        };
    }
    
    #endregion
}