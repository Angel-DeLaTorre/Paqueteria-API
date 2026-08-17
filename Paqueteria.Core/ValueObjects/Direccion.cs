using Paqueteria.Core.Entities.Catalogos;

namespace Paqueteria.Core.ValueObjects;

public record Direccion
{
    #region Columns
        public string Calle { get; init; } = string.Empty;
        public string NumeroExterior { get; init; }  = string.Empty;
        public string? NumeroInterior { get; init; }
        public string Colonia { get; init; }  = string.Empty;
        public string CodigoPostal { get; init; }  = string.Empty;
        public string? Localidad { get; init; }
        public Guid MunicipioId { get; init; } = Guid.Empty;
    
    #endregion
    
    #region Foreign Keys
        public Municipio Municipio { get; init; } = null!;
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