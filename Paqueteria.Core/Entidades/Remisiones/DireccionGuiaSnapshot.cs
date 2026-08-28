using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entidades.Remisiones;

public class DireccionGuiaSnapshot
{
    #region Column
        public Guid Id { get; init; }
        public Direccion Direccion { get; set; } = null!;
    #endregion
    
    #region Constructors
    
        private DireccionGuiaSnapshot () {}

        public static DireccionGuiaSnapshot Crear(Direccion direccion)
        {
            return new DireccionGuiaSnapshot()
            {
                Direccion = direccion,
            };
        }
    
    #endregion
}