using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Catalogos;

namespace Paqueteria.Core.ValueObjects;

[Owned]
public record Direccion
{
    #region Columns
    
        [MaxLength(255)]
        [Column("calle")]
        public string Calle { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("numero_exterior")]
        public string NumeroExterior { get; set; }  = string.Empty;

        [MaxLength(50)]
        [Column("numero_interior")]
        public string? NumeroInterior { get; set; }

        [MaxLength(100)]
        [Column("colonia")]
        public string Colonia { get; set; }  = string.Empty;

        [MaxLength(6)]
        [Column("codigo_postal")]
        public string CodigoPostal { get; set; }  = string.Empty;

        [MaxLength(100)]
        [Column("localidad")]
        public string? Localidad { get; set; }

        [Column("municipio_id")]
        public Guid MunicipioId { get; set; } = Guid.Empty;
    
    #endregion
    
    #region Foreign Keys
        public virtual Municipio Municipio { get; init; } = null!;
    #endregion
    
    #region Constructors

        private Direccion() {}
        
        public static Direccion Create(
            string calle, 
            string numeroExterior,
            string? numeroInterior,
            string colonia,
            string codigoPostal,
            string? localidad,
            Guid municipioId
            )
        {
            return new Direccion()
            {
                Calle = calle,
                NumeroExterior = numeroExterior,
                NumeroInterior = numeroInterior,
                Colonia = colonia,
                CodigoPostal = codigoPostal,
                Localidad = localidad,
                MunicipioId = municipioId,
            };
        }
        
    #endregion
}