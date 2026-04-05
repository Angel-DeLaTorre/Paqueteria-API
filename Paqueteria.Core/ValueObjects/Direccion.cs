using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Paqueteria.Core.ValueObjects;

[Owned]
public record Direccion
{
    #region Columns
    
        [MaxLength(255)]
        [Column("calle")]
        public string? Calle { get; set; }

        [MaxLength(50)]
        [Column("numero_exterior")]
        public string? NumeroExterior { get; set; }

        [MaxLength(50)]
        [Column("numero_interior")]
        public string? NumeroInterior { get; set; }

        [MaxLength(100)]
        [Column("colonia")]
        public string? Colonia { get; set; }

        [MaxLength(6)]
        [Column("codigo_postal")]
        public string? CodigoPostal { get; set; }

        [MaxLength(100)]
        [Column("localidad")]
        public string? Localidad { get; set; }

        [Column("municipio_id")]
        public Guid? MunicipioId { get; set; }
    
    #endregion
    
    #region Constructors

        private Direccion() {}
        
        public Direccion(string calle, string numeroExterior, string? numeroInterior, string colonia, string codigoPostal,
            string? localidad, Guid municipioId)
        {
            Calle = calle;
            NumeroExterior = numeroExterior;
            NumeroInterior = numeroInterior;
            Colonia = colonia;
            CodigoPostal = codigoPostal;
            Localidad = localidad;
            MunicipioId = municipioId;
        }
    #endregion
}